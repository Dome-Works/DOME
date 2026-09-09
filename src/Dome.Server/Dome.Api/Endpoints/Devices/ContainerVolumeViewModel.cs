using DeviceContainerVolumeDto = Dome.Shared.Containers.ContainerVolumeDto;

namespace Dome.Api.Endpoints.Devices;

public sealed record ContainerVolumeViewModel
{
    public string? Name { get; init; }

    public string? Source { get; init; }

    public required string Destination { get; init; }

    public string? Type { get; init; }

    public bool ReadOnly { get; init; }

    public long? SizeBytes { get; init; }

    public static ContainerVolumeViewModel From(DeviceContainerVolumeDto volume)
        => new()
        {
            Name = volume.Name,
            Source = volume.Source,
            Destination = volume.Destination,
            Type = volume.Type,
            ReadOnly = volume.ReadOnly,
            SizeBytes = volume.SizeBytes,
        };
}
