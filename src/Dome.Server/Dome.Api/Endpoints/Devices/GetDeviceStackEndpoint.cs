using FastEndpoints;
using Dome.Business.Stacks;

namespace Dome.Api.Endpoints.Devices;

public sealed class GetDeviceStackEndpoint : Endpoint<GetDeviceStackRequest, StackViewModel>
{
    private readonly IStackService _stackService;

    public GetDeviceStackEndpoint(IStackService stackService)
    {
        _stackService = stackService;
    }

    public override void Configure()
    {
        Get("/api/devices/{SocketName}/stacks/{StackId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDeviceStackRequest req, CancellationToken ct)
    {
        var stack = await _stackService.GetAsync(req.SocketName, req.StackId, ct);
        if (stack is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(StackViewModel.From(stack), ct);
    }
}
