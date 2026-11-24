using FastEndpoints;
using Dome.Business.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Dome.Api.Endpoints.Auth;

public sealed class LogoutEndpoint : EndpointWithoutRequest
{
    private readonly IAdminAccountService _adminAccountService;

    public LogoutEndpoint(IAdminAccountService adminAccountService)
    {
        _adminAccountService = adminAccountService;
    }

    public override void Configure()
    {
        Post("/api/auth/logout");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        await _adminAccountService.RevokeSessionsAsync(ct);
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await Send.NoContentAsync(ct);
    }
}
