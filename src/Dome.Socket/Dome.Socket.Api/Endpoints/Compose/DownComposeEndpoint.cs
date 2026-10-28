using FastEndpoints;
using Dome.Socket.Api.Docker.Compose;
using Dome.Socket.Contracts.Compose;

namespace Dome.Socket.Api.Endpoints.Compose;

public sealed class DownComposeEndpoint : Endpoint<ComposeDownRequest>
{
    private readonly IDockerComposeClient _dockerComposeClient;

    public DownComposeEndpoint(IDockerComposeClient dockerComposeClient)
    {
        _dockerComposeClient = dockerComposeClient;
    }

    public override void Configure()
    {
        Post("/api/compose/down");
        AllowAnonymous();
    }

    public override async Task HandleAsync(ComposeDownRequest req, CancellationToken ct)
    {
        var result = await _dockerComposeClient.DownAsync(req.ProjectName, req.RemoveVolumes, ct);
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
