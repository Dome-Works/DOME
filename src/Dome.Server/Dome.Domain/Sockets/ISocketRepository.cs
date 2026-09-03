namespace HomelabDocs.Domain.Sockets;

public interface ISocketRepository
{
    Task<IReadOnlyList<Socket>> ListAsync(CancellationToken cancellationToken = default);

    Task<Socket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Socket?> GetByNameAsync(string name, CancellationToken cancellationToken = default);

    Task<bool> NameExistsAsync(
        string name,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default);

    Task AddAsync(Socket socket, CancellationToken cancellationToken = default);

    Task UpdateAsync(Socket socket, CancellationToken cancellationToken = default);

    Task DeleteAsync(Socket socket, CancellationToken cancellationToken = default);
}
