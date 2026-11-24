namespace Dome.Socket.Api.Security;

internal static class PrivateFile
{
    private const UnixFileMode OwnerOnly = UnixFileMode.UserRead | UnixFileMode.UserWrite;

    public static void Write(string path, byte[] content)
    {
        var options = new FileStreamOptions { Mode = FileMode.Create, Access = FileAccess.Write };
        if (!OperatingSystem.IsWindows())
        {
            options.UnixCreateMode = OwnerOnly;
        }

        using (var stream = new FileStream(path, options))
        {
            stream.Write(content);
        }

        Restrict(path);
    }

    public static void Restrict(string path)
    {
        if (!OperatingSystem.IsWindows() && File.GetUnixFileMode(path) != OwnerOnly)
        {
            File.SetUnixFileMode(path, OwnerOnly);
        }
    }
}
