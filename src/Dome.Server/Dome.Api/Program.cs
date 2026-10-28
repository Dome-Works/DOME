using FastEndpoints;
using FastEndpoints.Swagger;
using Dome.Api.Telemetry;
using Dome.Business;
using Dome.Domain;
using Dome.Domain.Seeding;
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

app.UseCors();
app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
app.UseSwaggerGen();

app.Run();
