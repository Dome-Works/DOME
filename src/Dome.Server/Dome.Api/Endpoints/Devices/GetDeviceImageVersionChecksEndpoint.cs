using FastEndpoints;
using Dome.Business.Images;

namespace Dome.Api.Endpoints.Devices;

public sealed class GetDeviceImageVersionChecksEndpoint
    : Endpoint<GetDeviceImageVersionChecksRequest, GetDeviceImageVersionChecksResponse>
{
    private readonly IDeviceImageVersionChecker _deviceImageVersionChecker;

    public GetDeviceImageVersionChecksEndpoint(IDeviceImageVersionChecker deviceImageVersionChecker)
    {
        _deviceImageVersionChecker = deviceImageVersionChecker;
    }

    public override void Configure()
    {
        Get("/api/devices/{SocketName}/image-version-checks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDeviceImageVersionChecksRequest req, CancellationToken ct)
    {
        var checks = await _deviceImageVersionChecker.GetAsync(req.SocketName, ct);
        if (checks is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(
            new GetDeviceImageVersionChecksResponse
            {
                Checks = checks.Select(ContainerImageVersionCheckViewModel.From).ToArray(),
            },
            ct);
    }
}
