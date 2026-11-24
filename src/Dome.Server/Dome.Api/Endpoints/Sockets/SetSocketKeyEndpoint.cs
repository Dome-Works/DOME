using FastEndpoints;
using Dome.Business.Sockets;
using Dome.Shared.Sockets;

namespace Dome.Api.Endpoints.Sockets;

public sealed class SetSocketKeyEndpoint : Endpoint<SetSocketKeyRequest, SocketResponse>
{
    private readonly ISocketService _socketService;

    public SetSocketKeyEndpoint(ISocketService socketService)
    {
        _socketService = socketService;
    }

    public override void Configure()
    {
        Put("/api/sockets/{Id}/key");
    }

    public override async Task HandleAsync(SetSocketKeyRequest req, CancellationToken ct)
    {
        var result = await _socketService.SetKeyAsync(req.Id, req.Key, ct);
        if (result.IsNotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        if (!result.IsSuccess)
        {
            AddError(result.Error ?? "The request is invalid.");
            await Send.ErrorsAsync(cancellation: ct);
            return;
        }

        await Send.OkAsync(result.Socket!, ct);
    }
}
