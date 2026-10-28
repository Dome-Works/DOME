using FastEndpoints;
using Dome.Business.Devices;

namespace Dome.Api.Endpoints.Devices;

public sealed class DeleteDeviceUnmanagedStackEndpoint : Endpoint<DeleteDeviceUnmanagedStackRequest>
{
    private readonly IDeviceStackDeployService _deviceStackDeployService;

    public DeleteDeviceUnmanagedStackEndpoint(IDeviceStackDeployService deviceStackDeployService)
    {
        _deviceStackDeployService = deviceStackDeployService;
    }

    public override void Configure()
    {
        Delete("/api/devices/{SocketName}/unmanaged-stacks/{ProjectName}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(DeleteDeviceUnmanagedStackRequest req, CancellationToken ct)
    {
        var result = await _deviceStackDeployService.DeleteUnmanagedAsync(req.SocketName, req.ProjectName, req.RemoveVolumes, ct);
        if (result.IsDeviceNotFound || result.IsStackNotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        if (result.IsFailed)
        {
            AddError(result.Error ?? "Failed to delete stack containers.");
            await Send.ErrorsAsync(statusCode: 502, cancellation: ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
