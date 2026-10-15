namespace Dome.Api.Endpoints.Devices;

public sealed record GetDeviceImageVersionChecksResponse
{
    public IReadOnlyCollection<ContainerImageVersionCheckViewModel> Checks { get; init; }
        = Array.Empty<ContainerImageVersionCheckViewModel>();
}
