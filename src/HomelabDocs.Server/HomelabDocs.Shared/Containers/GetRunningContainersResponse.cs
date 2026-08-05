namespace HomelabDocs.Shared.Containers;

public sealed record GetRunningContainersResponse
{
    public IReadOnlyCollection<ContainerResponse> Containers { get; init; }
        = Array.Empty<ContainerResponse>();
}
