namespace Dome.Socket.Contracts.Containers;

public sealed record DeleteContainersRequest
{
    public IReadOnlyList<string> ContainerIds { get; init; } = Array.Empty<string>();

    public bool RemoveVolumes { get; init; }
}
