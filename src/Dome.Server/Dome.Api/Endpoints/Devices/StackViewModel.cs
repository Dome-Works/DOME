using Dome.Shared.Stacks;

namespace Dome.Api.Endpoints.Devices;

public sealed record StackViewModel
{
    public required Guid Id { get; init; }

    public required Guid SocketId { get; init; }

    public required string ProjectName { get; init; }

    public required string ComposeYaml { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required DateTimeOffset UpdatedAt { get; init; }

    public static StackViewModel From(StackDto stack)
        => new()
        {
            Id = stack.Id,
            SocketId = stack.SocketId,
            ProjectName = stack.ProjectName,
            ComposeYaml = stack.ComposeYaml,
            CreatedAt = stack.CreatedAt,
            UpdatedAt = stack.UpdatedAt
        };
}
