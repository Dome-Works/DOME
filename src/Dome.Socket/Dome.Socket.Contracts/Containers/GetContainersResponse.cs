namespace Dome.Socket.Contracts.Containers;

public sealed record GetContainersResponse
{
    public IReadOnlyCollection<ContainerResponse> Containers { get; init; }
        = Array.Empty<ContainerResponse>();
}
