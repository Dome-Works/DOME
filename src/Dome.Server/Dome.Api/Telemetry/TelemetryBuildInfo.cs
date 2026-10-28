using System.Reflection;

namespace Dome.Api.Telemetry;

/// <summary>
/// Telemetry values fixed at build time. They are deliberately not read from
/// configuration; only the opt-in flag is runtime-configurable.
/// </summary>
internal static class TelemetryBuildInfo
{
    public static readonly Uri HostUrl = new("https://eu.i.posthog.com");

    public const string ProjectToken = "phc_wQy7jJafVGJFJzFLf6J5vfcLj7XqP4DyZKNu5kWhdzoJ";

    public static string Environment { get; } = Read("DomeEnvironment") is { Length: > 0 } value ? value : "Dev";

    private static string Read(string key)
        => typeof(TelemetryBuildInfo).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attribute => attribute.Key == key)?.Value?.Trim() ?? string.Empty;
}
