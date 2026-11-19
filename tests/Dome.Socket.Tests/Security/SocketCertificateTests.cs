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
