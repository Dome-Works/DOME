using System.Reflection;

namespace Dome.Socket.Api;

public static class ApplicationVersion
{
    public static string? Current { get; } = Read(typeof(ApplicationVersion).Assembly);

    internal static string? Read(Assembly assembly)
    {
        var informational = assembly
            .GetCustomAttribute<AssemblyInformationalVersionAttribute>()
            ?.InformationalVersion;
        if (string.IsNullOrWhiteSpace(informational))
        {
            return null;
        }

        var plus = informational.IndexOf('+');
        return plus >= 0 ? informational[..plus] : informational;
    }
}
