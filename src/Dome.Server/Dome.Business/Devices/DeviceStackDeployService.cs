using System.Net;
using System.Text.Json;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Compose;
using Microsoft.Extensions.Logging;
using Refit;

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

    public async Task<DeviceStackDeployResult> DeployAsync(
        string socketName,
        Guid stackId,
        CancellationToken cancellationToken = default)
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
            await api.UpComposeAsync(
                new ComposeUpRequest
                {
                    ProjectName = stack.ProjectName,
                    ComposeYaml = stack.ComposeYaml
                },
                cancellationToken);
            return DeviceStackDeployResult.Success();
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.BadRequest)
        {
            return DeviceStackDeployResult.Failed(ReadSocketError(ex) ?? "The compose file is invalid.");
        }
        catch (ApiException ex) when ((int)ex.StatusCode == 502)
        {
            return DeviceStackDeployResult.Failed(
                ReadSocketError(ex) ?? "Docker Compose failed.");
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(
                ex,
                "Failed to deploy stack '{StackId}' on socket '{SocketName}' at '{Address}'.",
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
