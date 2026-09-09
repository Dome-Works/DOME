using FastEndpoints;
using Dome.Business.Devices;

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
                Containers = containers.Select(ContainerViewModel.From).ToArray()
            },
            ct);
    }
}
