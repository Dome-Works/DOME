using FastEndpoints;
using Dome.Business.Devices;

namespace Dome.Api.Endpoints.Devices;

public sealed class DeployDeviceStackEndpoint : Endpoint<GetDeviceStackRequest>
{
    private readonly IDeviceStackDeployService _deviceStackDeployService;

    public DeployDeviceStackEndpoint(IDeviceStackDeployService deviceStackDeployService)
    {
        _deviceStackDeployService = deviceStackDeployService;
    }

    public override void Configure()
    {
        Post("/api/devices/{SocketName}/stacks/{StackId}/deploy");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDeviceStackRequest req, CancellationToken ct)
    {
        var result = await _deviceStackDeployService.DeployAsync(req.SocketName, req.StackId, ct);
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
