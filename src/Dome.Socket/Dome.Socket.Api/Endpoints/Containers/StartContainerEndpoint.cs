using FastEndpoints;
using Dome.Socket.Api.Docker.Clients;

namespace Dome.Socket.Api.Endpoints.Containers;

public sealed class StartContainerEndpoint : Endpoint<ContainerLifecycleRequest>
{
    private readonly IDockerEngineClient _dockerEngineClient;

    public StartContainerEndpoint(IDockerEngineClient dockerEngineClient)
    {
        _dockerEngineClient = dockerEngineClient;
    }

    public override void Configure()
    {
        Post("/api/containers/{Id}/start");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ContainerLifecycleRequest req, CancellationToken ct)
    {
        var result = await _dockerEngineClient.StartContainerAsync(req.Id, ct);
        if (result == DockerContainerLifecycleResult.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
