using FastEndpoints;
using Dome.Socket.Api.Docker.Compose;
using Dome.Socket.Contracts.Compose;

namespace Dome.Socket.Api.Endpoints.Compose;

public sealed class UpComposeEndpoint : Endpoint<ComposeUpRequest>
{
    private readonly IDockerComposeClient _dockerComposeClient;

    public UpComposeEndpoint(IDockerComposeClient dockerComposeClient)
    {
        _dockerComposeClient = dockerComposeClient;
    }

    public override void Configure()
    {
        Post("/api/compose/up");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ComposeUpRequest req, CancellationToken ct)
    {
        var result = await _dockerComposeClient.UpAsync(req.ProjectName, req.ComposeYaml, ct);
        if (result.Succeeded)
        {
            await Send.NoContentAsync(ct);
            return;
        }

        var statusCode = result.IsInvalid ? 400 : 502;

        await Send.ResponseAsync(
            new ComposeCommandErrorResponse
            {
                Message = result.Error ?? "Docker Compose failed."
            },
            statusCode,
            cancellation: ct);
    }
}
