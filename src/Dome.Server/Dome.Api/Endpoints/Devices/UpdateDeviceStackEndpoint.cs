using FastEndpoints;
using Dome.Business.Stacks;

namespace Dome.Api.Endpoints.Devices;

public sealed class UpdateDeviceStackEndpoint
    : Endpoint<UpdateDeviceStackRequest, StackViewModel>
{
    private readonly IStackService _stackService;

    public UpdateDeviceStackEndpoint(IStackService stackService)
    {
        _stackService = stackService;
    }

    public override void Configure()
    {
        Put("/api/devices/{SocketName}/stacks/{StackId}");
        AllowAnonymous();
    }

    public override async Task HandleAsync(UpdateDeviceStackRequest req, CancellationToken ct)
    {
        var result = await _stackService.UpdateAsync(
            req.SocketName,
            req.StackId,
            req.ProjectName,
            req.ComposeYaml,
            ct);
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

        await Send.OkAsync(StackViewModel.From(result.Stack!), ct);
    }
}
