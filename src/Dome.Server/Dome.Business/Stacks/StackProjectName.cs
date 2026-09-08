using System.Text;

namespace Dome.Business.Stacks;

internal static class StackProjectName
{
    public static string FromDisplayName(string composeName)
    {
        var builder = new StringBuilder(composeName.Length);
        foreach (var character in composeName.Trim().ToLowerInvariant())
        {
            if (character is (>= 'a' and <= 'z') or (>= '0' and <= '9') or '_' or '-')
            {
                builder.Append(character);
            }
            else
            {
                builder.Append('-');
            }
        }

        return builder.ToString();
    }
}
