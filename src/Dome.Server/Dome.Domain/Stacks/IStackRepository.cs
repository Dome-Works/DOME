namespace Dome.Domain.Stacks;

public interface IStackRepository
{
    Task<IReadOnlyList<Stack>> ListBySocketIdAsync(
        Guid socketId,
        CancellationToken cancellationToken = default);

    Task<Stack?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<bool> ProjectNameExistsAsync(
        Guid socketId,
        string projectName,
        Guid? excludeId = null,
        CancellationToken cancellationToken = default);

    Task AddAsync(Stack stack, CancellationToken cancellationToken = default);

    Task UpdateAsync(Stack stack, CancellationToken cancellationToken = default);

    Task DeleteAsync(Stack stack, CancellationToken cancellationToken = default);
}
