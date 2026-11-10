using System.Text;
using FastEndpoints;
using FastEndpoints.Swagger;
using Dome.Socket.Api.Docker;
using Dome.Socket.Api.Security;

var builder = WebApplication.CreateBuilder(args);

var inContainer = builder.Configuration["DOTNET_RUNNING_IN_CONTAINER"] == "true";
var certificateDirectory = builder.Configuration["Socket:CertificateDirectory"]
    ?? (inContainer
        ? "/var/lib/dome-socket"
        : Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DomeSocket"));
var certificate = SocketCertificate.LoadOrCreate(certificateDirectory);
var security = new SocketSecurity(
    certificate,
    SocketCertificate.Sha256Fingerprint(certificate),
    Encoding.UTF8.GetBytes(builder.Configuration["DOME_SOCKET_KEY"] ?? string.Empty));

builder.WebHost.ConfigureKestrel(k => k.ListenAnyIP(inContainer ? 8080 : 5110, o => o.UseHttps(certificate)));

builder.Services.AddSingleton(security);
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDomeSocket(builder.Configuration);

var app = builder.Build();

if (security.Key.Length == 0)
{
    app.Logger.LogWarning("DOME_SOCKET_KEY is not set; all requests except POST /api/pair will be rejected with 401");
}

app.UseMiddleware<ApiKeyMiddleware>();
app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
app.UseSwaggerGen();

app.Run();
