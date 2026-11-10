using FastEndpoints;
using Dome.Business.Sockets;
using Dome.Shared.Sockets;

namespace Dome.Api.Endpoints.Sockets;

public sealed class RotateSocketKeyEndpoint : Endpoint<GetSocketRequest, SocketCreatedResponse>
{
    private readonly ISocketService _socketService;

    public RotateSocketKeyEndpoint(ISocketService socketService)
    {
        _socketService = socketService;
    }

    public override void Configure()
    {
        Post("/api/sockets/{Id}/key");
    }

    public override async Task HandleAsync(GetSocketRequest req, CancellationToken ct)
    {
        var result = await _socketService.RotateKeyAsync(req.Id, ct);
        if (!result.IsSuccess)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(SocketCreatedResponse.From(result.Socket!, result.ApiKey!), ct);
    }
}
