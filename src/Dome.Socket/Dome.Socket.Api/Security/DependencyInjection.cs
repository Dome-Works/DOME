using System.Text;

namespace Dome.Socket.Api.Security;

public static class DependencyInjection
{
    public static WebApplicationBuilder AddSocketSecurity(this WebApplicationBuilder builder)
    {
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

        return builder;
    }

    public static WebApplication UseSocketSecurity(this WebApplication app)
    {
        var socketKey = app.Services.GetRequiredService<SocketKey>();
        if (socketKey.IsPaired)
        {
            var keySource = socketKey.IsConfigured ? "DOME_SOCKET_KEY" : socketKey.KeyPath;
            app.Logger.LogInformation("Paired with DOME. The socket key is read from {KeySource}.", keySource);
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

        return app;
    }
}
