using System.Text.RegularExpressions;

namespace Dome.Business.Stacks;

internal static partial class StackProjectName
{
    public const int MaxLength = 128;

    [GeneratedRegex("^[a-z0-9][a-z0-9_-]*$", RegexOptions.CultureInvariant)]
    private static partial Regex Pattern();

    public static bool TryValidate(string? projectName, out string normalized, out string? error)
    {
        normalized = string.Empty;

        if (string.IsNullOrWhiteSpace(projectName))
        {
            error = "Project name is required.";
            return false;
        }

        var trimmed = projectName.Trim();
        if (trimmed.Length > MaxLength)
        {
            error = "Project name must be 128 characters or fewer.";
            return false;
        }

        if (!Pattern().IsMatch(trimmed))
        {
            error =
                "Project name must be a valid Compose project name: lowercase letters, digits, hyphens, and underscores, starting with a letter or digit.";
            return false;
        }

        normalized = trimmed;
        error = null;
        return true;
    }
}
