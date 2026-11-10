using FastEndpoints;
using Dome.Business.Auth;
using Dome.Shared.Auth;

namespace Dome.Api.Endpoints.Auth;

public sealed class SetupAdminEndpoint : Endpoint<SetupAdminRequest>
{
    private readonly IAdminAccountService _adminAccountService;

    public SetupAdminEndpoint(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }

    public override void Configure()
    {
        Post("/api/auth/setup");
        AllowAnonymous();
        Options(x => x.RequireRateLimiting("auth"));
    }

    public override async Task HandleAsync(SetupAdminRequest req, CancellationToken ct)
    {
        var result = await _adminAccountService.SetupAsync(req.Username, req.Password, ct);
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

        await HttpContext.SignInAdminAsync(req.Username.Trim());
        await Send.NoContentAsync(ct);
    }
}
