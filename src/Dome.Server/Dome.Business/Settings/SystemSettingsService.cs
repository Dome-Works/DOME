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
}
