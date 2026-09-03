using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Shared.Containers;
using Dome.Shared.Devices;
using Microsoft.Extensions.Logging;
using SocketContainer = Dome.Socket.Contracts.Containers.ContainerResponse;
using SocketVolume = Dome.Socket.Contracts.Containers.ContainerVolumeResponse;

namespace Dome.Business.Devices;

public sealed class DeviceQueryService : IDeviceQueryService
{
    private readonly ISocketRepository _socketRepository;
    private readonly IDomeSocketApiFactory _socketApiFactory;
    private readonly ILogger<DeviceQueryService> _logger;

    public DeviceQueryService(
        ISocketRepository socketRepository,
        IDomeSocketApiFactory socketApiFactory,
        ILogger<DeviceQueryService> logger)
    {
        _socketRepository = socketRepository;
        _socketApiFactory = socketApiFactory;
        _logger = logger;
    }

    public async Task<IReadOnlyList<DeviceResponse>> GetDevicesAsync(
        CancellationToken cancellationToken = default)
    {
        var sockets = await _socketRepository.ListAsync(cancellationToken);
        return sockets
            .Select(static socket => new DeviceResponse { Name = socket.Name })
            .ToArray();
    }

    public async Task<IReadOnlyCollection<ContainerDto>?> GetContainersAsync(
        string deviceName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(deviceName);

        var socket = await _socketRepository.GetByNameAsync(deviceName, cancellationToken);
        if (socket is null)
        {
            return null;
        }

        try
        {
            var api = _socketApiFactory.Create(socket.Address);
            var response = await api.GetContainersAsync(cancellationToken);
            return response.Containers
                .Select(Map)
                .ToArray();
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogError(
                ex,
                "Failed to list containers from socket '{SocketName}' at '{Address}'.",
                socket.Name,
                socket.Address);
            throw;
        }
    }

    private static ContainerDto Map(SocketContainer container)
        => new()
        {
            Id = container.Id,
            Name = container.Name,
            State = container.State,
            Stack = container.Stack,
            TotalBytes = container.Volumes.Sum(static volume => volume.SizeBytes ?? 0),
            Volumes = container.Volumes.Select(Map).ToArray(),
        };

    private static ContainerVolumeDto Map(SocketVolume volume)
        => new()
        {
            Name = volume.Name,
            Source = volume.Source,
            Destination = volume.Destination,
            Type = volume.Type,
            ReadOnly = volume.ReadOnly,
            SizeBytes = volume.SizeBytes,
        };
}
