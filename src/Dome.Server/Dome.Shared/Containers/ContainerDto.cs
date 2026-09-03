namespace Dome.Shared.Containers;

public sealed record ContainerDto
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string State { get; init; }

    public string? Stack { get; init; }

    public long TotalBytes { get; init; }

    public IReadOnlyCollection<ContainerVolumeDto> Volumes { get; init; }
        = Array.Empty<ContainerVolumeDto>();
}
