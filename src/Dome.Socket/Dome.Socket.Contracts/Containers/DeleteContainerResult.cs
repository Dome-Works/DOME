namespace Dome.Socket.Contracts.Containers;

public sealed record DeleteContainerResult
{
    public string ContainerId { get; init; } = string.Empty;

    public DeleteContainerStatus Status { get; init; }

    public string? Error { get; init; }
}
