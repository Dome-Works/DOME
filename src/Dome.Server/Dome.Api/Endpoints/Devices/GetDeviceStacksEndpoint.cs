using FastEndpoints;
using Dome.Business.Stacks;

namespace Dome.Api.Endpoints.Devices;

public sealed class GetDeviceStacksEndpoint
    : Endpoint<GetDeviceStacksRequest, GetDeviceStacksResponse>
{
    private readonly IStackService _stackService;

    public GetDeviceStacksEndpoint(IStackService stackService)
    {
        _stackService = stackService;
    }

    public override void Configure()
    {
        Get("/api/devices/{SocketName}/stacks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDeviceStacksRequest req, CancellationToken ct)
    {
        var stacks = await _stackService.ListAsync(req.SocketName, ct);
        if (stacks is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(
            new GetDeviceStacksResponse
            {
                Stacks = stacks.Select(StackViewModel.From).ToArray()
            },
            ct);
    }
}
