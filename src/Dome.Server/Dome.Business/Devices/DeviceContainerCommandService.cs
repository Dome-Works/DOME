using System.Net;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Socket.Contracts;
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
            static (api, id, ct) => api.StartContainerAsync(id, ct),
            "start",
            cancellationToken);

    public Task<DeviceContainerCommandResult> StopAsync(
        string deviceName,
        string containerId,
        CancellationToken cancellationToken = default)
        => ExecuteAsync(
            deviceName,
            containerId,
            static (api, id, ct) => api.StopContainerAsync(id, ct),
            "stop",
            cancellationToken);

    private async Task<DeviceContainerCommandResult> ExecuteAsync(
        string deviceName,
        string containerId,
        Func<IDomeSocketApi, string, CancellationToken, Task> operation,
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
            await operation(api, containerId, cancellationToken);
            return DeviceContainerCommandResult.Success();
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
