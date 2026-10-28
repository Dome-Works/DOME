namespace Dome.Shared.Settings;

public sealed record UpdateSystemSettingsRequest
{
    public bool TelemetryEnabled { get; init; }
}
