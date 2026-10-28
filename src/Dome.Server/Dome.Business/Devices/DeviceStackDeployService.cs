using System.Net;
using System.Text.Json;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Compose;
using Dome.Socket.Contracts.Containers;
using Microsoft.Extensions.Logging;
using Refit;

using StackEntity = Dome.Domain.Stacks.Stack;

namespace Dome.Business.Devices;

public sealed class DeviceStackDeployService : IDeviceStackDeployService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    private readonly ISocketRepository _socketRepository;
    private readonly IStackRepository _stackRepository;
    private readonly IDomeSocketApiFactory _socketApiFactory;
    private readonly ILogger<DeviceStackDeployService> _logger;

    public DeviceStackDeployService(
        ISocketRepository socketRepository,
        IStackRepository stackRepository,
        IDomeSocketApiFactory socketApiFactory,
        ILogger<DeviceStackDeployService> logger)
    {
        _socketRepository = socketRepository;
        _stackRepository = stackRepository;
        _socketApiFactory = socketApiFactory;
        _logger = logger;
    }

    public Task<DeviceStackDeployResult> DeployAsync(
        string socketName,
        Guid stackId,
        CancellationToken cancellationToken = default)
        => RunOnStackAsync(
            socketName,
            stackId,
            "deploy",
            "The compose file is invalid.",
            "Docker Compose failed.",
            (api, stack, ct) => api.UpComposeAsync(
                new ComposeUpRequest
                {
                    ProjectName = stack.ProjectName,
                    ComposeYaml = stack.ComposeYaml
                },
                ct),
            cancellationToken);

    public Task<DeviceStackDeployResult> DeleteAsync(
        string socketName,
        Guid stackId,
        bool removeVolumes,
        CancellationToken cancellationToken = default)
        => RunOnStackAsync(
            socketName,
            stackId,
            "delete",
            "The delete request is invalid.",
            "Docker Compose failed.",
            async (api, stack, ct) =>
            {
                await api.DownComposeAsync(
                    new ComposeDownRequest { ProjectName = stack.ProjectName, RemoveVolumes = removeVolumes },
                    ct);
                // Socket failures throw above, so the DB row is kept for a retry.
                await _stackRepository.DeleteAsync(stack, ct);
            },
            cancellationToken);

    public async Task<DeviceStackDeployResult> DeleteUnmanagedAsync(
        string socketName,
        string projectName,
        bool removeVolumes,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);
        if (string.IsNullOrWhiteSpace(projectName))
        {
            return DeviceStackDeployResult.StackNotFound();
        }

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        if (socket is null)
        {
            return DeviceStackDeployResult.DeviceNotFound();
        }

        var project = projectName.Trim();
        var managed = await _stackRepository.ListBySocketIdAsync(socket.Id, cancellationToken);
        if (managed.Any(s => string.Equals(s.ProjectName, project, StringComparison.OrdinalIgnoreCase)))
        {
            return DeviceStackDeployResult.StackNotFound();
        }

        try
        {
            var api = _socketApiFactory.Create(socket.Address);
            var containers = await api.GetContainersAsync(cancellationToken);
            var ids = containers.Containers
                .Where(c => string.Equals(c.Stack?.Trim(), project, StringComparison.OrdinalIgnoreCase))
                .Select(c => c.Id)
                .ToList();
            if (ids.Count == 0)
            {
                return DeviceStackDeployResult.StackNotFound();
            }

            var errors = new List<string>();
            foreach (var chunk in ids.Chunk(100))
            {
                var response = await api.DeleteContainersAsync(
                    new DeleteContainersRequest { ContainerIds = chunk, RemoveVolumes = removeVolumes },
                    cancellationToken);
                errors.AddRange(response.Results
                    .Where(r => r.Status == DeleteContainerStatus.Failed)
                    .Select(r => string.IsNullOrWhiteSpace(r.Error)
                        ? $"Socket failed to delete container '{r.ContainerId}'."
                        : r.Error));
            }

            return errors.Count > 0
                ? DeviceStackDeployResult.Failed(string.Join("; ", errors))
                : DeviceStackDeployResult.Success();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(
                ex,
                "Failed to delete unmanaged stack '{ProjectName}' on socket '{SocketName}' at '{Address}'.",
                project,
                socket.Name,
                socket.Address);
            throw;
        }
    }

    private async Task<DeviceStackDeployResult> RunOnStackAsync(
        string socketName,
        Guid stackId,
        string verb,
        string badRequestFallback,
        string badGatewayFallback,
        Func<IDomeSocketApi, StackEntity, CancellationToken, Task> socketCall,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        if (socket is null)
        {
            return DeviceStackDeployResult.DeviceNotFound();
        }

        var stack = await _stackRepository.GetByIdAsync(stackId, cancellationToken);
        if (stack is null || stack.SocketId != socket.Id)
        {
            return DeviceStackDeployResult.StackNotFound();
        }

        try
        {
            var api = _socketApiFactory.Create(socket.Address);
            await socketCall(api, stack, cancellationToken);
            return DeviceStackDeployResult.Success();
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            return DeviceStackDeployResult.Failed(ReadSocketError(ex) ?? badRequestFallback);
        }
        catch (ApiException ex) when ((int)ex.StatusCode == 502)
        {
            return DeviceStackDeployResult.Failed(ReadSocketError(ex) ?? badGatewayFallback);
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(
                ex,
                "Failed to {Verb} stack '{StackId}' on socket '{SocketName}' at '{Address}'.",
                verb,
                stackId,
                socket.Name,
                socket.Address);
            throw;
        }
    }

    private static string? ReadSocketError(ApiException exception)
    {
        if (string.IsNullOrWhiteSpace(exception.Content))
        {
            return null;
        }

        try
        {
            var body = JsonSerializer.Deserialize<ComposeCommandErrorResponse>(
                exception.Content,
                JsonOptions);
            return string.IsNullOrWhiteSpace(body?.Message) ? null : body.Message;
        }
        catch (JsonException)
        {
            return null;
        }
    }
}
