using FastEndpoints;
using Dome.Api.Telemetry;
using Dome.Business;
using Dome.Business.Settings;
using Dome.Shared.Settings;

namespace Dome.Api.Endpoints.Settings;

public sealed class GetSystemSettingsEndpoint : EndpointWithoutRequest<SystemSettingsResponse>
{
    private readonly ISystemSettingsService _settingsService;
    private readonly IApplicationVersion _applicationVersion;

    public GetSystemSettingsEndpoint(
        ISystemSettingsService settingsService,
        IApplicationVersion applicationVersion)
    {
        _settingsService = settingsService;
        _applicationVersion = applicationVersion;
    }

    public override void Configure()
    {
        Get("/api/settings");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var telemetry = await _settingsService.GetTelemetryAsync(ct);
        await Send.OkAsync(
            new SystemSettingsResponse
            {
                TelemetryEnabled = telemetry.Enabled,
                InstallationId = telemetry.InstallationId,
                Version = _applicationVersion.Current,
                Environment = TelemetryBuildInfo.Environment
            },
            ct);
    }
}
