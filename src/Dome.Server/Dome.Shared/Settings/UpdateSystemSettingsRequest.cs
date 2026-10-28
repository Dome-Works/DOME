namespace Dome.Shared.Settings;

public sealed record UpdateSystemSettingsRequest
{
    // Null fields are left unchanged.
    public bool? TelemetryEnabled { get; init; }

    public bool? OnboardingCompleted { get; init; }
}
