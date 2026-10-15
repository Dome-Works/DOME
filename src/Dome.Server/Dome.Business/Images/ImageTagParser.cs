using System.Globalization;
using System.Text.RegularExpressions;

namespace Dome.Business.Images;

public static class ImageTagParser
{
    private static readonly Regex VersionPattern = new(
        """^v?(?<major>\d+)\.(?<minor>\d+)(?:\.(?<patch>\d+))?(?<suffix>.*)$""",
        RegexOptions.CultureInvariant | RegexOptions.ExplicitCapture);

    public static ImageTagVersion? TryParse(string? tag)
    {
        if (string.IsNullOrWhiteSpace(tag))
        {
            return null;
        }

        var match = VersionPattern.Match(tag.Trim());
        if (!match.Success)
        {
            return null;
        }

        return new ImageTagVersion
        {
            Major = int.Parse(match.Groups["major"].Value, CultureInfo.InvariantCulture),
            Minor = int.Parse(match.Groups["minor"].Value, CultureInfo.InvariantCulture),
            Patch = match.Groups["patch"].Success
                ? int.Parse(match.Groups["patch"].Value, CultureInfo.InvariantCulture)
                : 0,
            Suffix = match.Groups["suffix"].Value,
            Original = tag.Trim(),
        };
    }

    public static int Compare(ImageTagVersion left, ImageTagVersion right)
    {
        ArgumentNullException.ThrowIfNull(left);
        ArgumentNullException.ThrowIfNull(right);

        var major = left.Major.CompareTo(right.Major);
        if (major != 0)
        {
            return major;
        }

        var minor = left.Minor.CompareTo(right.Minor);
        return minor != 0 ? minor : left.Patch.CompareTo(right.Patch);
    }
}
