using FastEndpoints;
using Dome.Business.Devices;

namespace Dome.Api.Endpoints.Devices;

public sealed class DeleteDeviceContainerEndpoint : Endpoint<DeleteDeviceContainerRequest>
{
    private readonly IDeviceContainerCommandService _deviceContainerCommandService;

    public DeleteDeviceContainerEndpoint(IDeviceContainerCommandService deviceContainerCommandService)
    {
        _deviceContainerCommandService = deviceContainerCommandService;
    }

    public override void Configure()
    {
        Delete("/api/devices/{Name}/containers/{ContainerId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(
        DeleteDeviceContainerRequest req,
        CancellationToken ct)
    {
        var result = await _deviceContainerCommandService.DeleteAsync(
            req.Name,
            req.ContainerId,
            req.RemoveVolumes,
            ct);

        if (result.IsDeviceNotFound || result.IsContainerNotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
