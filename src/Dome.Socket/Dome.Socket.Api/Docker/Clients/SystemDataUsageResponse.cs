namespace Dome.Socket.Api.Docker.Clients;

internal sealed record SystemDataUsageResponse(
    SystemDataUsageVolumeResponse[] Volumes);
