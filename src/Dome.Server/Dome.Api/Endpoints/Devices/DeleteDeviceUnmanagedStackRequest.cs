using FastEndpoints;

namespace Dome.Api.Endpoints.Devices;

public sealed class DeleteDeviceUnmanagedStackRequest
{
    public string SocketName { get; init; } = string.Empty;

    public string ProjectName { get; init; } = string.Empty;

    [QueryParam]
    public bool RemoveVolumes { get; init; }
}
