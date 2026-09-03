using FastEndpoints;
using Dome.Business.Sockets;
using Dome.Shared.Sockets;

namespace Dome.Api.Endpoints.Sockets;

public sealed class GetSocketStatusesEndpoint : EndpointWithoutRequest<GetSocketStatusesResponse>
{
    private readonly ISocketService _socketService;

    public GetSocketStatusesEndpoint(ISocketService socketService)
    {
        _socketService = socketService;
    }

    public override void Configure()
    {
        Get("/api/sockets/statuses");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var statuses = await _socketService.GetStatusesAsync(ct);
        await Send.OkAsync(
            new GetSocketStatusesResponse
            {
                Statuses = statuses
            },
            ct);
    }
}
