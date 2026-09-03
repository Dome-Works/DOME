using FastEndpoints;
using Dome.Business.Devices;
using DeviceContainerDto = Dome.Shared.Containers.ContainerDto;
using DeviceContainerVolumeDto = Dome.Shared.Containers.ContainerVolumeDto;

namespace Dome.Api.Endpoints.Devices;

public sealed class GetDeviceContainersEndpoint
    : Endpoint<GetDeviceContainersRequest, GetDeviceContainersResponse>
{
    private readonly IDeviceQueryService _deviceQueryService;

    public GetDeviceContainersEndpoint(IDeviceQueryService deviceQueryService)
    {
        _deviceQueryService = deviceQueryService;
    }

    public override void Configure()
    {
        Get("/api/devices/{Name}/containers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        GetDeviceContainersRequest req,
        CancellationToken ct)
    {
        var containers = await _deviceQueryService.GetContainersAsync(req.Name, ct);
        if (containers is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(
            new GetDeviceContainersResponse
            {
                Containers = containers.Select(Map).ToArray()
            },
            ct);
    }

    private static ContainerViewModel Map(DeviceContainerDto container)
        => new()
        {
            Id = container.Id,
            Name = container.Name,
            State = container.State,
            Stack = container.Stack,
            TotalBytes = container.TotalBytes,
            Volumes = container.Volumes.Select(Map).ToArray(),
        };

    private static ContainerVolumeViewModel Map(DeviceContainerVolumeDto volume)
        => new()
        {
            Name = volume.Name,
            Source = volume.Source,
            Destination = volume.Destination,
            Type = volume.Type,
            ReadOnly = volume.ReadOnly,
            SizeBytes = volume.SizeBytes,
        };
}
