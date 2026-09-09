namespace Dome.Api.Endpoints.Devices;

public sealed class CreateDeviceStackRequest
{
    public string SocketName { get; init; } = string.Empty;

    public string ProjectName { get; init; } = string.Empty;
}
