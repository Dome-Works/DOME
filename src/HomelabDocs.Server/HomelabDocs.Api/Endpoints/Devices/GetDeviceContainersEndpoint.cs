using FastEndpoints;
using HomelabDocs.Business.Docker.Clients;
using HomelabDocs.Shared.Containers;

namespace HomelabDocs.Api.Endpoints.Devices;

public sealed class GetDeviceContainersEndpoint
    : Endpoint<GetDeviceContainersRequest, GetRunningContainersResponse>
{
    private readonly IDockerClientRegistry _dockerClientRegistry;
    private readonly IDockerContainerClient _dockerContainerClient;

    public GetDeviceContainersEndpoint(
        IDockerClientRegistry dockerClientRegistry,
        IDockerContainerClient dockerContainerClient)
    {
        _dockerClientRegistry = dockerClientRegistry;
        _dockerContainerClient = dockerContainerClient;
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
        if (!_dockerClientRegistry.TryGetClient(req.Name, out _))
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        var containers = await _dockerContainerClient.GetRunningContainersAsync(
            req.Name,
            ct);

        await Send.OkAsync(
            new GetRunningContainersResponse
            {
                Containers = containers
            },
            ct);
    }
}
