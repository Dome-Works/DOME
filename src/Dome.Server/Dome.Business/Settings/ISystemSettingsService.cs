namespace Dome.Business.Settings;

public interface ISystemSettingsService
{
    Task<TelemetrySettings> GetTelemetryAsync(CancellationToken cancellationToken = default);

    Task SetTelemetryEnabledAsync(bool enabled, CancellationToken cancellationToken = default);

    Task<bool> GetOnboardingCompletedAsync(CancellationToken cancellationToken = default);

    Task SetOnboardingCompletedAsync(bool completed, CancellationToken cancellationToken = default);
}
