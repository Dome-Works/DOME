using System.Net;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Containers;
using Microsoft.Extensions.Logging;
using Refit;

namespace Dome.Business.Devices;

public sealed class DeviceContainerCommandService : IDeviceContainerCommandService
{
    private readonly ISocketRepository _socketRepository;
    private readonly IDomeSocketApiFactory _socketApiFactory;
    private readonly ILogger<DeviceContainerCommandService> _logger;

    public DeviceContainerCommandService(
        ISocketRepository socketRepository,
        IDomeSocketApiFactory socketApiFactory,
        ILogger<DeviceContainerCommandService> logger)
    {
        _socketRepository = socketRepository;
        _socketApiFactory = socketApiFactory;
        _logger = logger;
    }

    public Task<DeviceContainerCommandResult> StartAsync(
        string deviceName,
        string containerId,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(
            deviceName,
            containerId,
            async (api, id, ct) =>
            {
                await api.StartContainerAsync(id, ct);
                return DeviceContainerCommandResult.Success();
            },
            "start",
            cancellationToken);

    public Task<DeviceContainerCommandResult> StopAsync(
        string deviceName,
        string containerId,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(
            deviceName,
            containerId,
            async (api, id, ct) =>
            {
                await api.StopContainerAsync(id, ct);
                return DeviceContainerCommandResult.Success();
            },
            "stop",
            cancellationToken);

    public Task<DeviceContainerCommandResult> DeleteAsync(
        string deviceName,
        string containerId,
        bool removeVolumes,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(
            deviceName,
            containerId,
            async (api, id, ct) =>
            {
                var response = await api.DeleteContainersAsync(
                    new() { ContainerIds = [id], RemoveVolumes = removeVolumes },
                    ct);
                var result = response.Results.FirstOrDefault(r => r.ContainerId == id)
                    ?? (response.Results.Count == 1 ? response.Results[0] : null)
                    ?? throw new InvalidOperationException("Socket returned no delete result for the container.");

                return result.Status switch
                {
                    DeleteContainerStatus.Deleted => DeviceContainerCommandResult.Success(),
                    DeleteContainerStatus.NotFound => DeviceContainerCommandResult.ContainerNotFound(),
                    _ => throw new InvalidOperationException(result.Error ?? "Socket failed to delete the container."),
                };
            },
            "delete",
            cancellationToken);

    private async Task<DeviceContainerCommandResult> ExecuteAsync(
        string deviceName,
        string containerId,
        Func<IDomeSocketApi, string, CancellationToken, Task<DeviceContainerCommandResult>> operation,
        string action,
        CancellationToken cancellationToken)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(deviceName);
        ArgumentException.ThrowIfNullOrWhiteSpace(containerId);
        ArgumentNullException.ThrowIfNull(operation);

        var socket = await _socketRepository.GetByNameAsync(deviceName, cancellationToken);
        if (socket is null)
        {
            return DeviceContainerCommandResult.DeviceNotFound();
        }

        try
        {
            var api = _socketApiFactory.Create(socket.Address);
            return await operation(api, containerId, cancellationToken);
        }
        catch (ApiException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            return DeviceContainerCommandResult.ContainerNotFound();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(
                ex,
                "Failed to {Action} container '{ContainerId}' on socket '{SocketName}' at '{Address}'.",
                action,
                containerId,
                socket.Name,
                socket.Address);
            throw;
        }
    }
}
