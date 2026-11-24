using FastEndpoints;
using FastEndpoints.Swagger;
using Dome.Api.Endpoints.Auth;
using Dome.Api.Telemetry;
using Dome.Business;
using Dome.Business.Auth;
using Dome.Domain;
using Dome.Domain.Seeding;
using Dome.Domain.Sqlite;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;
using PostHog;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(
                "http://localhost:5173",
                "https://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Join(DomeDataDirectory.Resolve(builder.Configuration), "keys")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
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
builder.Services.AddAuthorization();

builder.Services.Configure<ForwardedHeadersOptions>(options =>
{
    options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    options.KnownIPNetworks.Clear();
    options.KnownProxies.Clear();
});

builder.Services.AddRateLimiter(options =>
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

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDomeBusiness();
builder.Services.AddDomeDomain(builder.Configuration);

if (TelemetryBuildInfo.ProjectToken.Length > 0)
{
    builder.AddPostHog();
    builder.Services.PostConfigure<PostHogOptions>(options =>
    {
        options.ProjectToken = TelemetryBuildInfo.ProjectToken;
        options.HostUrl = TelemetryBuildInfo.HostUrl;
    });
    builder.Services.AddHostedService<TelemetryReporter>();
}

var app = builder.Build();

await using (var scope = app.Services.CreateAsyncScope())
{
    var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
    await initializer.InitializeAsync();

    if (string.Equals(app.Configuration["DOME_RESET_ADMIN"], "true", StringComparison.OrdinalIgnoreCase))
    {
        await scope.ServiceProvider.GetRequiredService<IAdminAccountService>().ResetAsync();
        app.Logger.LogWarning("DOME_RESET_ADMIN is set: the admin account was reset and setup is open again. Remove the variable and restart after creating the new account.");
    }
}

app.UseExceptionHandler(errorApp => errorApp.Run(async context =>
{
    var error = context.Features.Get<IExceptionHandlerFeature>()?.Error;
    for (var ex = error; ex is not null; ex = ex.InnerException)
    {
        if (ex is HttpRequestException)
        {
            context.Response.StatusCode = StatusCodes.Status502BadGateway;
            await context.Response.WriteAsJsonAsync(new { message = "The socket could not be reached. Check its status on the Sockets page." });
            return;
        }
    }

    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
}));
app.UseForwardedHeaders();
app.UseCors();
app.Use(async (context, next) =>
{
    var request = context.Request;
    if (request.Path.StartsWithSegments("/api")
        && !HttpMethods.IsGet(request.Method)
        && !HttpMethods.IsHead(request.Method)
        && !HttpMethods.IsOptions(request.Method)
        && !request.Headers.ContainsKey("X-DOME-Request"))
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        return;
    }

    await next();
});
app.UseRateLimiter();
app.UseAuthentication();
app.UseAuthorization();
app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
app.UseSwaggerGen();

app.Run();
