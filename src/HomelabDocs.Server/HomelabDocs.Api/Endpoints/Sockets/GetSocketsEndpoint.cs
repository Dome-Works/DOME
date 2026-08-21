using FastEndpoints;
using HomelabDocs.Business.Sockets;
using HomelabDocs.Shared.Sockets;

namespace HomelabDocs.Api.Endpoints.Sockets;

public sealed class GetSocketsEndpoint : EndpointWithoutRequest<GetSocketsResponse>
{
    private readonly ISocketService _socketService;

    public GetSocketsEndpoint(ISocketService socketService)
    {
        _socketService = socketService;
    }

    public override void Configure()
    {
        Get("/api/sockets");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var sockets = await _socketService.ListAsync(ct);
        await Send.OkAsync(
            new GetSocketsResponse
            {
                Sockets = sockets
            },
            ct);
    }
}
