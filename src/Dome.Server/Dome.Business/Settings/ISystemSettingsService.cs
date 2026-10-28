namespace Dome.Business.Settings;

public interface ISystemSettingsService
{
    Task<TelemetrySettings> GetTelemetryAsync(CancellationToken cancellationToken = default);

    Task SetTelemetryEnabledAsync(bool enabled, CancellationToken cancellationToken = default);
}
