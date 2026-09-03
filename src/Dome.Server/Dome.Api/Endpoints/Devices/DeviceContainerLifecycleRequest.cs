namespace Dome.Api.Endpoints.Devices;

public sealed class DeviceContainerLifecycleRequest
{
    public string Name { get; init; } = string.Empty;

    public string ContainerId { get; init; } = string.Empty;
}
