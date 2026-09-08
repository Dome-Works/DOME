using Microsoft.EntityFrameworkCore;

namespace Dome.Domain.Stacks;

internal sealed class StackRepository : IStackRepository
{
    private readonly DomeDbContext _db;

    public StackRepository(DomeDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<Stack>> ListBySocketIdAsync(
        Guid socketId,
        CancellationToken cancellationToken = default)
    {
        return await _db.Stacks
            .AsNoTracking()
            .Where(stack => stack.SocketId == socketId)
            .OrderBy(stack => stack.ComposeName)
            .ToListAsync(cancellationToken);
    }

    public Task<Stack?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return _db.Stacks.FirstOrDefaultAsync(stack => stack.Id == id, cancellationToken);
    }

    public Task<bool> ProjectNameExistsAsync(
        Guid socketId,
        string projectName,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default)
    {
        var normalized = projectName.ToLower();
        var query = _db.Stacks
            .AsNoTracking()
            .Where(stack =>
                stack.SocketId == socketId &&
                stack.ProjectName.ToLower() == normalized);
        if (excludeId is { } id)
        {
            query = query.Where(stack => stack.Id != id);
        }

        return query.AnyAsync(cancellationToken);
    }

    public async Task AddAsync(Stack stack, CancellationToken cancellationToken = default)
    {
        _db.Stacks.Add(stack);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task UpdateAsync(Stack stack, CancellationToken cancellationToken = default)
        => _db.SaveChangesAsync(cancellationToken);

    public async Task DeleteAsync(Stack stack, CancellationToken cancellationToken = default)
    {
        _db.Stacks.Remove(stack);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
