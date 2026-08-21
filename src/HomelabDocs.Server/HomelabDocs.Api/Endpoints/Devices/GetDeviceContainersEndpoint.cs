using FastEndpoints;
using HomelabDocs.Business.Devices;
using HomelabDocs.Shared.Containers;

namespace HomelabDocs.Api.Endpoints.Devices;

public sealed class GetDeviceContainersEndpoint
    : Endpoint<GetDeviceContainersRequest, GetRunningContainersResponse>
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
            new GetRunningContainersResponse
            {
                Containers = containers
            },
            ct);
    }
}
