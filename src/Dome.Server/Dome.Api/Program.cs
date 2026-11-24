using FastEndpoints;
using FastEndpoints.Swagger;
using Dome.Api.Auth;
using Dome.Api.Telemetry;
using Dome.Business;
using Dome.Domain;
using Dome.Domain.Seeding;
using Microsoft.AspNetCore.Diagnostics;
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

builder.Services.AddDomeAuthentication(builder.Configuration);
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
}

await app.ResetAdminIfRequestedAsync();

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
app.UseCors();
app.UseDomeAuthentication();
app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
app.UseSwaggerGen();

app.Run();
