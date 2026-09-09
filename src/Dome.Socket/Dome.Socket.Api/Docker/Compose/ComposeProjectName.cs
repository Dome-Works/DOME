using System.Text.RegularExpressions;

namespace Dome.Socket.Api.Docker.Compose;

internal static partial class ComposeProjectName
{
    public const int MaxLength = 128;

    [GeneratedRegex("^[a-z0-9][a-z0-9_-]*$", RegexOptions.CultureInvariant)]
    private static partial Regex Pattern();

    public static bool IsValid(string? projectName)
    {
        if (string.IsNullOrWhiteSpace(projectName))
        {
            return false;
        }

        var trimmed = projectName.Trim();
        return trimmed.Length <= MaxLength && Pattern().IsMatch(trimmed);
    }
}
