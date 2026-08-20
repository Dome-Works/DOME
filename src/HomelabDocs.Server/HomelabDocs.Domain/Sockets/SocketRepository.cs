using Microsoft.EntityFrameworkCore;

namespace HomelabDocs.Domain.Sockets;

internal sealed class SocketRepository : ISocketRepository
{
    private readonly HomelabDocsDbContext _db;

    public SocketRepository(HomelabDocsDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Socket>> ListAsync(CancellationToken cancellationToken = default)
    {
        return await _db.Sockets
            .AsNoTracking()
            .OrderBy(socket => socket.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Socket?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _db.Sockets.FirstOrDefaultAsync(socket => socket.Id == id, cancellationToken);
    }

    public Task<Socket?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
    {
        var normalized = name.ToLower();
        return _db.Sockets.FirstOrDefaultAsync(
            socket => socket.Name.ToLower() == normalized,
            cancellationToken);
    }

    public Task<bool> NameExistsAsync(
        string name,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default)
    {
        var normalized = name.ToLower();
        var query = _db.Sockets.AsNoTracking().Where(socket => socket.Name.ToLower() == normalized);
        if (excludeId is { } id)
        {
            query = query.Where(socket => socket.Id != id);
        }

        return query.AnyAsync(cancellationToken);
    }

    public async Task AddAsync(Socket socket, CancellationToken cancellationToken = default)
    {
        _db.Sockets.Add(socket);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Socket socket, CancellationToken cancellationToken = default)
        => _db.SaveChangesAsync(cancellationToken);

    public async Task DeleteAsync(Socket socket, CancellationToken cancellationToken = default)
    {
        _db.Sockets.Remove(socket);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
