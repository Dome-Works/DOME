using FastEndpoints;

namespace Dome.Api.Endpoints.Devices;

public sealed class DeleteDeviceStackRequest
{
    public string SocketName { get; init; } = string.Empty;

    public Guid StackId { get; init; }

    [QueryParam]
    public bool RemoveVolumes { get; init; }
}
