using System.Reflection;

namespace Dome.Business;

public sealed class AssemblyApplicationVersion : IApplicationVersion
{
    public AssemblyApplicationVersion()
        : this(typeof(AssemblyApplicationVersion).Assembly)
    {
    }

    internal AssemblyApplicationVersion(Assembly assembly)
    {
        Current = Read(assembly) ?? string.Empty;
    }

    public string Current { get; }

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
