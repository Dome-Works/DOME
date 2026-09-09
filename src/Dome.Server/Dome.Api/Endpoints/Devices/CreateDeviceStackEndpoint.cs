using FastEndpoints;
using Dome.Business.Stacks;

namespace Dome.Api.Endpoints.Devices;

public sealed class CreateDeviceStackEndpoint
    : Endpoint<CreateDeviceStackRequest, StackViewModel>
{
    private readonly IStackService _stackService;

    public CreateDeviceStackEndpoint(IStackService stackService)
    {
        _stackService = stackService;
    }

    public override void Configure()
    {
        Post("/api/devices/{SocketName}/stacks");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CreateDeviceStackRequest req, CancellationToken ct)
    {
        var result = await _stackService.CreateAsync(req.SocketName, req.ProjectName, ct);
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

        await Send.CreatedAtAsync<GetDeviceStackEndpoint>(
            new { SocketName = req.SocketName, StackId = result.Stack!.Id },
            StackViewModel.From(result.Stack),
            cancellation: ct);
    }
}
