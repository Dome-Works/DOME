using FastEndpoints;
using Dome.Socket.Api.Docker.Clients;
using Dome.Socket.Contracts.Health;

namespace Dome.Socket.Api.Endpoints.Health;

public sealed class GetHealthEndpoint : EndpointWithoutRequest<SocketHealthResponse>
{
    private readonly IDockerEngineClient _dockerEngineClient;

    public GetHealthEndpoint(IDockerEngineClient dockerEngineClient)
    {
        _dockerEngineClient = dockerEngineClient;
    }

    public override void Configure()
    {
        Get("/api/health");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var reachable = await _dockerEngineClient.IsReachableAsync(ct);
        await Send.OkAsync(
            new SocketHealthResponse
            {
                DockerReachable = reachable
            },
            ct);
    }
}
