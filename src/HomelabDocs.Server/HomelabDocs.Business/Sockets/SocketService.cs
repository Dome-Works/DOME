To resolve GitHub issue #48, we need to add a healthcheck for Sockets and implement a "live" status bubble on the Socket page. The solution involves adding a method to check the liveness of a socket by attempting a TCP connection to its address. This method will be used by the UI to display the live status bubble.

Changes made:
1. Added `using System.Net.Sockets;` for TCP client functionality.
2. Added a new `CheckLivenessAsync` method that:
   - Retrieves the socket by ID
   - Parses the address into host and port
   - Attempts a TCP connection with a 5-second timeout
   - Returns true if connection succeeds, false otherwise
   - Handles exceptions and invalid address formats gracefully

Here's the complete updated file:

```csharp
using HomelabDocs.Domain.Sockets;
using HomelabDocs.Shared.Sockets;
using System.Net.Sockets;
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

        var trimmedName = name.Trim();
        var trimmedAddress = address.Trim();

        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        if (socket is null)
        {
            return SocketMutationResult.NotFound();
        }

        if (await _socketRepository.NameExistsAsync(trimmedName, excludeId: id, cancellationToken))
        {
            return SocketMutationResult.Conflict(
                $"A socket named '{trimmedName}' already exists.");
        }

        socket.Name = trimmedName;
        socket.Address = trimmedAddress;

        await _socketRepository.UpdateAsync(socket, cancellationToken);
        return SocketMutationResult.Success(Map(socket));
    }

    public async Task<SocketMutationResult> DeleteAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        if (socket is null)
        {
            return SocketMutationResult.NotFound();
        }

        await _socketRepository.DeleteAsync(socket, cancellationToken);
        return SocketMutationResult.Success();
    }

    public async Task<bool> CheckLivenessAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var socket = await _socketRepository.GetByIdAsync(id, cancellationToken);
        if (socket is null)
        {
            return false;
        }

        var addressParts = socket.Address.Split(':');
        if (addressParts.Length != 2 ||
            !ushort.TryParse(addressParts[1], out var port))
        {
            return false;
        }

        var host = address
