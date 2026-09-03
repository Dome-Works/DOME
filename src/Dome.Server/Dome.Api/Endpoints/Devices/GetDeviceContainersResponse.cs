namespace Dome.Api.Endpoints.Devices;

public sealed record GetDeviceContainersResponse
{
    public IReadOnlyCollection<ContainerViewModel> Containers { get; init; }
        = Array.Empty<ContainerViewModel>();
}
