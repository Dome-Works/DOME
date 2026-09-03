using FastEndpoints;
using Dome.Business.Sockets;
using Dome.Shared.Sockets;

namespace Dome.Api.Endpoints.Sockets;

public sealed class CreateSocketEndpoint : Endpoint<CreateSocketRequest, SocketResponse>
{
    private readonly ISocketService _socketService;

    public CreateSocketEndpoint(ISocketService socketService)
    {
        _socketService = socketService;
    }

    public override void Configure()
    {
        Post("/api/sockets");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateSocketRequest req, CancellationToken ct)
    {
        var result = await _socketService.CreateAsync(req.Name, req.Address, ct);
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

        await Send.CreatedAtAsync<GetSocketEndpoint>(
            new { result.Socket!.Id },
            result.Socket,
            cancellation: ct);
    }
}
