using FastEndpoints;
using Dome.Business.Sockets;
using Dome.Shared.Sockets;

namespace Dome.Api.Endpoints.Sockets;

public sealed class UpdateSocketEndpoint : Endpoint<UpdateSocketRequest, SocketResponse>
{
    private readonly ISocketService _socketService;

    public UpdateSocketEndpoint(ISocketService socketService)
    {
        _socketService = socketService;
    }

    public override void Configure()
    {
        Put("/api/sockets/{Id}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateSocketRequest req, CancellationToken ct)
    {
        var result = await _socketService.UpdateAsync(req.Id, req.Name, req.Address, ct);
        if (result.IsNotFound)
        {
            await Send.NotFoundAsync(ct);
            return;
        }

        if (result.IsConflict)
        {
            AddError(result.Error!);
            await Send.ErrorsAsync(statusCode: 409, cancellation: ct);
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
