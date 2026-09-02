using FastEndpoints;
using HomelabDocs.Socket.Api.Docker.Clients;

namespace HomelabDocs.Socket.Api.Endpoints.Containers;

public sealed class StopContainerEndpoint : Endpoint<ContainerLifecycleRequest>
{
    private readonly IDockerEngineClient _dockerEngineClient;

    public StopContainerEndpoint(IDockerEngineClient dockerEngineClient)
    {
        _dockerEngineClient = dockerEngineClient;
    }

    public override void Configure()
    {
        Post("/api/containers/{Id}/stop");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ContainerLifecycleRequest req, CancellationToken ct)
    {
        var result = await _dockerEngineClient.StopContainerAsync(req.Id, ct);
        if (result == DockerContainerLifecycleResult.NotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
