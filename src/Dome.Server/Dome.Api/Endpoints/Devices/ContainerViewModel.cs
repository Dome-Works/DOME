using DeviceContainerDto = Dome.Shared.Containers.ContainerDto;
using DeviceContainerVolumeDto = Dome.Shared.Containers.ContainerVolumeDto;

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

    public static ContainerViewModel From(DeviceContainerDto container)
        => new()
        {
            Id = container.Id,
            Name = container.Name,
            State = container.State,
            Stack = container.Stack,
            TotalBytes = container.TotalBytes,
            Volumes = container.Volumes.Select(ContainerVolumeViewModel.From).ToArray(),
        };
}
