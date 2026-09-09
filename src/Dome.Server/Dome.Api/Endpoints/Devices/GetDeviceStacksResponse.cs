namespace Dome.Api.Endpoints.Devices;

public sealed record GetDeviceStacksResponse
{
    public IReadOnlyCollection<StackViewModel> Stacks { get; init; }
        = Array.Empty<StackViewModel>();
}
