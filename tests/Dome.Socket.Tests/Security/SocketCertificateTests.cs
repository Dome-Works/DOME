using Dome.Socket.Api.Security;

namespace Dome.Socket.Tests.Security;

public sealed class SocketCertificateTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));

    public void Dispose()
    {
        if (Directory.Exists(_directory))
        {
            Directory.Delete(_directory, true);
        }
    }

    [Fact]
    public void LoadOrCreate_creates_pfx_with_private_key()
    {
        using var certificate = SocketCertificate.LoadOrCreate(_directory);

        Assert.True(File.Exists(Path.Combine(_directory, "socket.pfx")));
        Assert.True(certificate.HasPrivateKey);
        Assert.Equal("CN=dome-socket", certificate.Subject);
    }

    [Fact]
    public void Key_and_pfx_are_owner_only_and_existing_files_are_restricted()
    {
        if (OperatingSystem.IsWindows())
        {
            return;
        }

        const UnixFileMode ownerOnly = UnixFileMode.UserRead | UnixFileMode.UserWrite;
        var pfx = Path.Combine(_directory, "socket.pfx");
        var key = Path.Combine(_directory, SocketKey.KeyFileName);

        SocketCertificate.LoadOrCreate(_directory).Dispose();
        SocketKey.LoadOrCreate(_directory, null);
        Assert.Equal(ownerOnly, File.GetUnixFileMode(pfx));
        Assert.Equal(ownerOnly, File.GetUnixFileMode(key));

        File.SetUnixFileMode(pfx, (UnixFileMode)0b110_100_100);
        File.SetUnixFileMode(key, (UnixFileMode)0b110_100_100);
        SocketCertificate.LoadOrCreate(_directory).Dispose();
        SocketKey.LoadOrCreate(_directory, null);
        Assert.Equal(ownerOnly, File.GetUnixFileMode(pfx));
        Assert.Equal(ownerOnly, File.GetUnixFileMode(key));
    }

    [Fact]
    public void LoadOrCreate_returns_same_certificate_on_second_call()
    {
        using var first = SocketCertificate.LoadOrCreate(_directory);
        using var second = SocketCertificate.LoadOrCreate(_directory);

        Assert.Equal(first.Thumbprint, second.Thumbprint);
        Assert.True(second.HasPrivateKey);
    }

    [Fact]
    public void Sha256Fingerprint_is_32_bytes_and_stable()
    {
        using var first = SocketCertificate.LoadOrCreate(_directory);
        using var second = SocketCertificate.LoadOrCreate(_directory);

        var fingerprint = SocketCertificate.Sha256Fingerprint(first);

        Assert.Equal(32, fingerprint.Length);
        Assert.Equal(fingerprint, SocketCertificate.Sha256Fingerprint(second));
    }
}
