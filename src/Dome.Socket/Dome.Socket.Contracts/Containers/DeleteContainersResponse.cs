namespace Dome.Socket.Contracts.Containers;

public sealed record DeleteContainersResponse
{
    public IReadOnlyList<DeleteContainerResult> Results { get; init; }
        = Array.Empty<DeleteContainerResult>();
}
