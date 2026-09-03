namespace Dome.Api.Endpoints.Devices;

public sealed record ContainerViewModel
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required string State { get; init; }

    public string? Stack { get; init; }

    public long TotalBytes { get; init; }

    public IReadOnlyCollection<ContainerVolumeViewModel> Volumes { get; init; }
        = Array.Empty<ContainerVolumeViewModel>();
}
