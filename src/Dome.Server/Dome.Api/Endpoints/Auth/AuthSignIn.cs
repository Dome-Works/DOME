using System.Security.Claims;
using Dome.Business.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Dome.Api.Endpoints.Auth;

internal static class AuthSignIn
{
    public const string SessionVersionClaim = "dome:session_version";

    public static Task SignInAdminAsync(this HttpContext httpContext, string username, string sessionVersion)
        => httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
                [new Claim(ClaimTypes.Name, username), new Claim(SessionVersionClaim, sessionVersion)],
                CookieAuthenticationDefaults.AuthenticationScheme)));

    public static async Task ValidateSessionVersionAsync(CookieValidatePrincipalContext context)
    {
        var accounts = context.HttpContext.RequestServices.GetRequiredService<IAdminAccountService>();
        var current = await accounts.GetSessionVersionAsync(context.HttpContext.RequestAborted);
        if (context.Principal?.FindFirst(SessionVersionClaim)?.Value == current)
            return;

        context.RejectPrincipal();
        await context.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}
