using FastEndpoints;
using HomelabDocs.Business.Docker.Clients;
using HomelabDocs.Shared.Containers;

namespace HomelabDocs.Api.Endpoints.Containers;

public sealed class GetRunningContainersEndpoint : EndpointWithoutRequest<GetRunningContainersResponse>
{
    private readonly IDockerContainerClient _dockerContainerClient;

    public GetRunningContainersEndpoint(IDockerContainerClient dockerContainerClient)
    {
        _dockerContainerClient = dockerContainerClient;
    }

    public override void Configure()
    {
        Get("/api/containers");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var containers = await _dockerContainerClient.GetRunningContainersAsync(ct);

        await Send.OkAsync(
            new GetRunningContainersResponse
            {
                Containers = containers
            },
            ct);
    }
}