using HomelabDocs.Business.Sockets;
using HomelabDocs.Domain.Sockets;
using HomelabDocs.Shared.Containers;
using HomelabDocs.Shared.Devices;
using Microsoft.Extensions.Logging;
using SocketContainer = HomelabDocs.Socket.Contracts.Containers.ContainerResponse;

namespace HomelabDocs.Business.Devices;

public sealed class DeviceQueryService : IDeviceQueryService
{
    private readonly ISocketRepository _socketRepository;
    private readonly IHomelabDocsSocketApiFactory _socketApiFactory;
    private readonly ILogger<DeviceQueryService> _logger;

    public DeviceQueryService(
        ISocketRepository socketRepository,
        IHomelabDocsSocketApiFactory socketApiFactory,
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

    public async Task<IReadOnlyCollection<ContainerResponse>?> GetContainersAsync(
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

    private static ContainerResponse Map(SocketContainer container)
        => new()
        {
            Id = container.Id,
            Name = container.Name,
            State = container.State,
            Stack = container.Stack
        };
}
