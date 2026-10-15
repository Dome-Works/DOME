using FastEndpoints;
using Dome.Socket.Api.Docker.Images;
using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Api.Endpoints.Images;

public sealed class CheckImageVersionsEndpoint
    : Endpoint<CheckImageVersionsRequest, CheckImageVersionsResponse>
{
    private readonly IImageVersionCheckService _imageVersionCheckService;

    public CheckImageVersionsEndpoint(IImageVersionCheckService imageVersionCheckService)
    {
        _imageVersionCheckService = imageVersionCheckService;
    }

    public override void Configure()
    {
        Post("/api/images/version-checks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CheckImageVersionsRequest req, CancellationToken ct)
    {
        var response = await _imageVersionCheckService.CheckAsync(req, ct);
        await Send.OkAsync(response, ct);
    }
}
