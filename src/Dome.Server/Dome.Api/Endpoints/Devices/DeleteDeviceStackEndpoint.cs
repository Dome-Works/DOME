using FastEndpoints;
using Dome.Business.Stacks;

namespace Dome.Api.Endpoints.Devices;

public sealed class DeleteDeviceStackEndpoint : Endpoint<GetDeviceStackRequest>
{
    private readonly IStackService _stackService;

    public DeleteDeviceStackEndpoint(IStackService stackService)
    {
        _stackService = stackService;
    }

    public override void Configure()
    {
        Delete("/api/devices/{SocketName}/stacks/{StackId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDeviceStackRequest req, CancellationToken ct)
    {
        var deleted = await _stackService.DeleteAsync(req.SocketName, req.StackId, ct);
        if (deleted is not true)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
