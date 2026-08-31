namespace HomelabDocs.Api.Endpoints.Devices;

public sealed record ContainerVolumeViewModel
{
    public string? Name { get; init; }

    public string? Source { get; init; }

    public required string Destination { get; init; }

    public string? Type { get; init; }

    public bool ReadOnly { get; init; }

    public long? SizeBytes { get; init; }
}
