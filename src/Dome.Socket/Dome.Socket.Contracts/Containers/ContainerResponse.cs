namespace Dome.Socket.Contracts.Containers;

public sealed record ContainerResponse
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string State { get; init; }

    public string? Stack { get; init; }

    public IReadOnlyCollection<ContainerVolumeResponse> Volumes { get; init; }
        = Array.Empty<ContainerVolumeResponse>();
}
