using FastEndpoints;
using Dome.Business.Devices;

namespace Dome.Api.Endpoints.Devices;

public sealed class DeleteDeviceStackEndpoint : Endpoint<DeleteDeviceStackRequest>
{
    private readonly IDeviceStackDeployService _deviceStackDeployService;

    public DeleteDeviceStackEndpoint(IDeviceStackDeployService deviceStackDeployService)
    {
        _deviceStackDeployService = deviceStackDeployService;
    }

    public override void Configure()
    {
        Delete("/api/devices/{SocketName}/stacks/{StackId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteDeviceStackRequest req, CancellationToken ct)
    {
        var result = await _deviceStackDeployService.DeleteAsync(req.SocketName, req.StackId, req.RemoveVolumes, ct);
        if (result.IsDeviceNotFound || result.IsStackNotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        if (result.IsFailed)
        {
            AddError(result.Error ?? "Docker Compose failed.");
            await Send.ErrorsAsync(statusCode: 502, cancellation: ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
