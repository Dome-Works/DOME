using FastEndpoints;
using Dome.Business.Devices;
using Dome.Shared.Devices;

namespace Dome.Api.Endpoints.Devices;

public sealed class GetDevicesEndpoint : EndpointWithoutRequest<GetDevicesResponse>
{
    private readonly IDeviceQueryService _deviceQueryService;

    public GetDevicesEndpoint(IDeviceQueryService deviceQueryService)
    {
        _deviceQueryService = deviceQueryService;
    }

    public override void Configure()
    {
        Get("/api/devices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var devices = await _deviceQueryService.GetDevicesAsync(ct);
        await Send.OkAsync(
            new GetDevicesResponse
            {
                Devices = devices
            },
            ct);
    }
}
