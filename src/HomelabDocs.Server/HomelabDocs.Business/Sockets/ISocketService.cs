using HomelabDocs.Shared.Sockets;

namespace HomelabDocs.Business.Sockets;

public interface ISocketService
{
    Task<IReadOnlyList<SocketResponse>> ListAsync(CancellationToken cancellationToken = default);

    Task<SocketResponse?> GetAsync(Guid id, CancellationToken cancellationToken = default);

    Task<SocketMutationResult> CreateAsync(
        string name,
        string address,
        CancellationToken cancellationToken = default);

    Task<SocketMutationResult> UpdateAsync(
        Guid id,
        string name,
        string address,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SocketStatusResponse>> GetStatusesAsync(
        CancellationToken cancellationToken = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}
