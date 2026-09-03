using FastEndpoints;
using Dome.Business.Devices;

namespace Dome.Api.Endpoints.Devices;

public sealed class StartDeviceContainerEndpoint : Endpoint<DeviceContainerLifecycleRequest>
{
    private readonly IDeviceContainerCommandService _deviceContainerCommandService;

    public StartDeviceContainerEndpoint(IDeviceContainerCommandService deviceContainerCommandService)
    {
        _deviceContainerCommandService = deviceContainerCommandService;
    }

    public override void Configure()
    {
        Post("/api/devices/{Name}/containers/{ContainerId}/start");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeviceContainerLifecycleRequest req,
        CancellationToken ct)
    {
        var result = await _deviceContainerCommandService.StartAsync(
            req.Name,
            req.ContainerId,
            ct);

        if (result.IsDeviceNotFound || result.IsContainerNotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
