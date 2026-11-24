using System.Threading.RateLimiting;
using Dome.Api.Endpoints.Auth;
using Dome.Business.Auth;
using Dome.Domain.Sqlite;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.HttpOverrides;

namespace Dome.Api.Auth;

public static class DependencyInjection
{
    private const string RequestHeader = "X-DOME-Request";

    public static IServiceCollection AddDomeAuthentication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDataProtection()
            .PersistKeysToFileSystem(new DirectoryInfo(Path.Join(DomeDataDirectory.Resolve(configuration), "keys")));

        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.Cookie.Name = "dome_session";
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
                options.ExpireTimeSpan = TimeSpan.FromDays(7);
                options.SlidingExpiration = true;
                options.Events.OnValidatePrincipal = AuthSignIn.ValidateSessionVersionAsync;
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };
                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    return Task.CompletedTask;
                };
            });
        services.AddAuthorization();

        services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            options.KnownIPNetworks.Clear();
            options.KnownProxies.Clear();
        });

        services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            options.AddPolicy("auth", context => RateLimitPartition.GetFixedWindowLimiter(
                context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 5,
                    Window = TimeSpan.FromMinutes(1),
                    QueueLimit = 0
                }));
        });

        return services;
    }

    public static IApplicationBuilder UseDomeAuthentication(this IApplicationBuilder app)
    {
        app.UseForwardedHeaders();
        app.Use(async (context, next) =>
        {
            var request = context.Request;
            if (request.Path.StartsWithSegments("/api")
                && !HttpMethods.IsGet(request.Method)
                && !HttpMethods.IsHead(request.Method)
                && !HttpMethods.IsOptions(request.Method)
                && !request.Headers.ContainsKey(RequestHeader))
            {
                context.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }

            await next();
        });
        app.UseRateLimiter();
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }

    public static async Task ResetAdminIfRequestedAsync(this WebApplication app)
    {
        if (!string.Equals(app.Configuration["DOME_RESET_ADMIN"], "true", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        await using var scope = app.Services.CreateAsyncScope();
        await scope.ServiceProvider.GetRequiredService<IAdminAccountService>().ResetAsync();
        app.Logger.LogWarning("DOME_RESET_ADMIN is set: the admin account was reset and setup is open again. Remove the variable and restart after creating the new account.");
    }
}
