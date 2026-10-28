using Dome.Domain.Settings;

namespace Dome.Business.Settings;

internal sealed class SystemSettingsService : ISystemSettingsService
{
    private readonly ISystemSettingRepository _repository;

    public SystemSettingsService(ISystemSettingRepository repository)
    {
        _repository = repository;
    }

    public async Task<TelemetrySettings> GetTelemetryAsync(CancellationToken cancellationToken = default)
    {
        var enabled = await _repository.GetAsync(SystemSettingKeys.TelemetryEnabled, cancellationToken);
        var installationId = await _repository.GetAsync(SystemSettingKeys.InstallationId, cancellationToken);

        // Missing or unparsable values mean opted out.
        return new TelemetrySettings(
            bool.TryParse(enabled, out var parsed) && parsed && !string.IsNullOrWhiteSpace(installationId),
            installationId ?? string.Empty);
    }

    public Task SetTelemetryEnabledAsync(bool enabled, CancellationToken cancellationToken = default)
        => _repository.SetAsync(SystemSettingKeys.TelemetryEnabled, enabled.ToString(), cancellationToken);

    public async Task<bool> GetOnboardingCompletedAsync(CancellationToken cancellationToken = default)
    {
        var completed = await _repository.GetAsync(SystemSettingKeys.OnboardingCompleted, cancellationToken);
        return bool.TryParse(completed, out var parsed) && parsed;
    }

    public Task SetOnboardingCompletedAsync(bool completed, CancellationToken cancellationToken = default)
        => _repository.SetAsync(SystemSettingKeys.OnboardingCompleted, completed.ToString(), cancellationToken);
}
