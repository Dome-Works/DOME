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
var socketKey = SocketKey.LoadOrCreate(certificateDirectory, builder.Configuration["DOME_SOCKET_KEY"]);
var security = new SocketSecurity(
    certificate,
    SocketCertificate.Sha256Fingerprint(certificate),
    Encoding.UTF8.GetBytes(socketKey.Value));

builder.WebHost.ConfigureKestrel(k => k.ListenAnyIP(inContainer ? 8080 : 5110, o => o.UseHttps(certificate)));

builder.Services.AddSingleton(security);
builder.Services.AddSingleton(socketKey);
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument();
builder.Services.AddDomeSocket(builder.Configuration);

var app = builder.Build();

if (socketKey.IsPaired)
{
    app.Logger.LogInformation(
        "Paired with DOME. The socket key is read from {KeySource}.",
        socketKey.IsConfigured ? "DOME_SOCKET_KEY" : socketKey.KeyPath);
}
else
{
    app.Logger.LogWarning(
        """

        ################################################################
        #  DOME SOCKET KEY
        #
        #  Paste this key into DOME when you register this socket:
        #
        #      {SocketKey}
        #
        #  This message repeats at every start until DOME has paired.
        ################################################################
        """,
        socketKey.Value);
}

app.UseMiddleware<ApiKeyMiddleware>();
app.UseFastEndpoints(c =>
{
    c.Serializer.Options.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
});
app.UseSwaggerGen();

app.Run();
