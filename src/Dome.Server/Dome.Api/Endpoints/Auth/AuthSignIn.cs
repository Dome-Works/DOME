using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Dome.Api.Endpoints.Auth;

internal static class AuthSignIn
{
    public static Task SignInAdminAsync(this HttpContext httpContext, string username)
        => httpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            new ClaimsPrincipal(new ClaimsIdentity(
                [new Claim(ClaimTypes.Name, username)],
                CookieAuthenticationDefaults.AuthenticationScheme)));
}
