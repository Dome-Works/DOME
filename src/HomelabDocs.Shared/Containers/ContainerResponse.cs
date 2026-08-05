namespace HomelabDocs.Shared.Containers;

public sealed record ContainerResponse
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string Image { get; init; }

    public required string State { get; init; }

    public required string Status { get; init; }

    public IReadOnlyCollection<ContainerPortResponse> Ports { get; init; }
        = Array.Empty<ContainerPortResponse>();
}
