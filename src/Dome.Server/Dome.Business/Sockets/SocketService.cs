using Dome.Domain.Sockets;
using Dome.Shared.Sockets;
using Microsoft.Extensions.Logging;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Sockets;

public sealed class SocketService : ISocketService
{
    private readonly ISocketRepository _socketRepository;
    private readonly IDomeSocketApiFactory _socketApiFactory;
    private readonly ILogger<SocketService> _logger;

    public SocketService(
        ISocketRepository socketRepository,
        IDomeSocketApiFactory socketApiFactory,
        ILogger<SocketService> logger)
    {
        _socketRepository = socketRepository;
        _socketApiFactory = socketApiFactory;
        _logger = logger;
    }

    public async Task<IReadOnlyList<SocketResponse>> ListAsync(
        CancellationToken cancellationToken = default)
    {
        var sockets = await _socketRepository.ListAsync(cancellationToken);
        return sockets.Select(Map).ToArray();
    }

    public async Task<SocketResponse?> GetAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        return socket is null ? null : Map(socket);
    }

    public async Task<SocketMutationResult> CreateAsync(
        string name,
        string address,
        CancellationToken cancellationToken = default)
    {
        var validationError = Validate(name, address);
        if (validationError is not null)
        {
            return SocketMutationResult.Invalid(validationError);
        }

        var trimmedName = name.Trim();
        var trimmedAddress = address.Trim();

        if (await _socketRepository.NameExistsAsync(trimmedName, excludeId: null, cancellationToken))
        {
            return SocketMutationResult.Conflict(
                $"A socket named '{trimmedName}' already exists.");
        }

        var socket = new SocketEntity
        {
            Id = Guid.NewGuid(),
            Name = trimmedName,
            Address = trimmedAddress,
            CreatedAt = DateTimeOffset.UtcNow
        };

        await _socketRepository.AddAsync(socket, cancellationToken);
        return SocketMutationResult.Success(Map(socket));
    }

    public async Task<SocketMutationResult> UpdateAsync(
        Guid id,
        string name,
        string address,
        CancellationToken cancellationToken = default)
    {
        var validationError = Validate(name, address);
        if (validationError is not null)
        {
            return SocketMutationResult.Invalid(validationError);
        }

        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        if (socket is null)
        {
            return SocketMutationResult.NotFound();
        }

        var trimmedName = name.Trim();
        var trimmedAddress = address.Trim();

        if (await _socketRepository.NameExistsAsync(trimmedName, id, cancellationToken))
        {
            return SocketMutationResult.Conflict(
                $"A socket named '{trimmedName}' already exists.");
        }

        socket.Name = trimmedName;
        socket.Address = trimmedAddress;
        await _socketRepository.UpdateAsync(socket, cancellationToken);
        return SocketMutationResult.Success(Map(socket));
    }

    public async Task<IReadOnlyList<SocketStatusResponse>> GetStatusesAsync(
        CancellationToken cancellationToken = default)
    {
        var sockets = await _socketRepository.ListAsync(cancellationToken);
        var statuses = await Task.WhenAll(
            sockets.Select(socket => GetStatusAsync(socket, cancellationToken)));

        return statuses;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        if (socket is null)
        {
            return false;
        }

        await _socketRepository.DeleteAsync(socket, cancellationToken);
        return true;
    }

    private static string? Validate(string name, string address)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return "Name is required.";
        }

        return SocketAddressValidator.TryValidate(address, out var error)
            ? null
            : error;
    }

    private static SocketResponse Map(SocketEntity socket)
        => new()
        {
            Id = socket.Id,
            Name = socket.Name,
            Address = socket.Address,
            CreatedAt = socket.CreatedAt
        };

    private async Task<SocketStatusResponse> GetStatusAsync(
        SocketEntity socket,
        CancellationToken cancellationToken)
    {
        try
        {
            var api = _socketApiFactory.Create(socket.Address);
            await api.GetHealthAsync(cancellationToken);
            return new SocketStatusResponse
            {
                Id = socket.Id,
                IsReachable = true
            };
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(
                ex,
                "Failed to reach socket '{SocketName}' at '{Address}'.",
                socket.Name,
                socket.Address);

            return new SocketStatusResponse
            {
                Id = socket.Id,
                IsReachable = false
            };
        }
    }
}
