using FastEndpoints;
using HomelabDocs.Business.Docker.Clients;
using HomelabDocs.Shared.Devices;

namespace HomelabDocs.Api.Endpoints.Devices;

public sealed class GetDevicesEndpoint : EndpointWithoutRequest<GetDevicesResponse>
{
    private readonly IDockerClientRegistry _dockerClientRegistry;

    public GetDevicesEndpoint(IDockerClientRegistry dockerClientRegistry)
    {
        _dockerClientRegistry = dockerClientRegistry;
    }

    public override void Configure()
    {
        Get("/api/devices");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await Send.OkAsync(
            new GetDevicesResponse
            {
                Devices = _dockerClientRegistry.GetDevices()
            },
            ct);
    }
}
