using HomelabDocs.Domain.Sockets;
using HomelabDocs.Shared.Sockets;
using SocketEntity = HomelabDocs.Domain.Sockets.Socket;

namespace HomelabDocs.Business.Sockets;

public sealed class SocketService : ISocketService
{
    private readonly ISocketRepository _socketRepository;

    public SocketService(ISocketRepository socketRepository)
    {
        _socketRepository = socketRepository;
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
