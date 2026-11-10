using FastEndpoints;
using Dome.Business.Auth;
using Dome.Shared.Auth;

namespace Dome.Api.Endpoints.Auth;

public sealed class LoginEndpoint : Endpoint<LoginRequest>
{
    private readonly IAdminAccountService _adminAccountService;

    public LoginEndpoint(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }

    public override void Configure()
    {
        Post("/api/auth/login");
        AllowAnonymous();
        Options(x => x.RequireRateLimiting("auth"));
    }

    public override async Task HandleAsync(LoginRequest req, CancellationToken ct)
    {
        var username = await _adminAccountService.ValidateAsync(req.Username, req.Password, ct);
        if (username is null)
        {
            AddError("Invalid username or password.");
            await Send.ErrorsAsync(statusCode: 401, cancellation: ct);
            return;
        }

        await HttpContext.SignInAdminAsync(username);
        await Send.NoContentAsync(ct);
    }
}
