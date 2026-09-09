using Dome.Shared.Stacks;

namespace Dome.Business.Stacks;

public interface IStackService
{
    Task<IReadOnlyList<StackDto>?> ListAsync(
        string socketName,
        CancellationToken cancellationToken = default);

    Task<StackDto?> GetAsync(
        string socketName,
        Guid stackId,
        CancellationToken cancellationToken = default);

    Task<StackMutationResult> CreateAsync(
        string socketName,
        string projectName,
        CancellationToken cancellationToken = default);

    Task<StackMutationResult> UpdateAsync(
        string socketName,
        Guid stackId,
        string projectName,
        string composeYaml,
        CancellationToken cancellationToken = default);

    Task<bool?> DeleteAsync(
        string socketName,
        Guid stackId,
        CancellationToken cancellationToken = default);
}
