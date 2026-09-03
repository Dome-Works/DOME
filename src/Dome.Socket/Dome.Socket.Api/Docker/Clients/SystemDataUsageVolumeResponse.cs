namespace Dome.Socket.Api.Docker.Clients;

internal sealed class SystemDataUsageVolumeResponse
{
    public string? Name { get; init; }

    public SystemDataUsageVolumeUsageDataResponse? UsageData { get; init; }
}
