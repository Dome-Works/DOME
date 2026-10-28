namespace Dome.Shared.Settings;

public sealed record SystemSettingsResponse
{
    public bool TelemetryEnabled { get; init; }

    public bool OnboardingCompleted { get; init; }

    public string InstallationId { get; init; } = string.Empty;

    public string Version { get; init; } = string.Empty;

    public string Environment { get; init; } = string.Empty;
}
