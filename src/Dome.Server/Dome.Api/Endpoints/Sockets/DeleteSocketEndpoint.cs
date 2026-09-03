using FastEndpoints;
using HomelabDocs.Business.Sockets;
using HomelabDocs.Shared.Sockets;

namespace HomelabDocs.Api.Endpoints.Sockets;

public sealed class DeleteSocketEndpoint : Endpoint<GetSocketRequest>
{
    private readonly ISocketService _socketService;

    public DeleteSocketEndpoint(ISocketService socketService)
    {
        _socketService = socketService;
    }

    public override void Configure()
    {
        Delete("/api/sockets/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetSocketRequest req, CancellationToken ct)
    {
        var deleted = await _socketService.DeleteAsync(req.Id, ct);
        if (!deleted)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        await Send.NoContentAsync(ct);
    }
}
