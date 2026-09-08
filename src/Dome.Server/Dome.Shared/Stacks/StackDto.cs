namespace Dome.Shared.Stacks;

public sealed record StackDto
{
    public required Guid Id { get; init; }

    public required Guid SocketId { get; init; }

    public required string ProjectName { get; init; }

    public required string ComposeYaml { get; init; }

    public required DateTimeOffset CreatedAt { get; init; }

    public required DateTimeOffset UpdatedAt { get; init; }
}
