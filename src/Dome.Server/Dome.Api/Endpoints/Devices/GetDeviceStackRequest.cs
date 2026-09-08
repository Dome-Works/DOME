namespace Dome.Api.Endpoints.Devices;

public sealed class GetDeviceStackRequest
{
    public string SocketName { get; init; } = string.Empty;

    public Guid StackId { get; init; }
}
