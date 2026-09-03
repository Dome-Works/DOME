using FastEndpoints;
using Dome.Socket.Api.Docker.Clients;
using Dome.Socket.Contracts.Containers;

namespace Dome.Socket.Api.Endpoints.Containers;

public sealed class GetContainersEndpoint : EndpointWithoutRequest<GetContainersResponse>
{
    private readonly IDockerEngineClient _dockerEngineClient;

    public GetContainersEndpoint(IDockerEngineClient dockerEngineClient)
    {
        _dockerEngineClient = dockerEngineClient;
    }

    public override void Configure()
    {
        Get("/api/containers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var containers = await _dockerEngineClient.ListContainersAsync(ct);
        await Send.OkAsync(
            new GetContainersResponse
            {
                Containers = containers
            },
            ct);
    }
}
