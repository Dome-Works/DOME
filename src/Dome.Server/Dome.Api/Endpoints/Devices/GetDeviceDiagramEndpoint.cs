using FastEndpoints;
using Dome.Business.Devices;

namespace Dome.Api.Endpoints.Devices;

public sealed class GetDeviceDiagramEndpoint
    : Endpoint<GetDeviceDiagramRequest, GetDeviceDiagramResponse>
{
    private readonly IDeviceDiagramService _deviceDiagramService;

    public GetDeviceDiagramEndpoint(IDeviceDiagramService deviceDiagramService)
    {
        _deviceDiagramService = deviceDiagramService;
    }

    public override void Configure()
    {
        Get("/api/devices/{SocketName}/diagram");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDeviceDiagramRequest req, CancellationToken ct)
    {
        var diagram = await _deviceDiagramService.GetDiagramAsync(req.SocketName, ct);
        if (diagram is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(
            new GetDeviceDiagramResponse
            {
                Stacks = diagram.Stacks.Select(DiagramStackViewModel.From).ToArray(),
                Containers = diagram.Containers.Select(ContainerViewModel.From).ToArray()
            },
            ct);
    }
}
