using FastEndpoints;
using Dome.Business.Sockets;
using Dome.Shared.Sockets;

namespace Dome.Api.Endpoints.Sockets;

public sealed class GetSocketEndpoint : Endpoint<GetSocketRequest, SocketResponse>
{
    private readonly ISocketService _socketService;

    public GetSocketEndpoint(ISocketService socketService)
    {
        _socketService = socketService;
    }

    public override void Configure()
    {
        Get("/api/sockets/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSocketRequest req, CancellationToken ct)
    {
        var socket = await _socketService.GetAsync(req.Id, ct);
        if (socket is null)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.OkAsync(socket, ct);
    }
}
