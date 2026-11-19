using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Dome.Socket.Api.Security;

public static class SocketCertificate
{
    private const string FileName = "socket.pfx";

    public static X509Certificate2 LoadOrCreate(string directory)
    {
        Directory.CreateDirectory(directory);
        var path = Path.Combine(directory, FileName);

        if (File.Exists(path))
        {
            return X509CertificateLoader.LoadPkcs12FromFile(path, null);
        }

        using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        var request = new CertificateRequest("CN=dome-socket", key, HashAlgorithmName.SHA256);
        request.CertificateExtensions.Add(new X509BasicConstraintsExtension(false, false, 0, true));
        request.CertificateExtensions.Add(new X509EnhancedKeyUsageExtension(
            [new Oid("1.3.6.1.5.5.7.3.1")], false));

        var now = DateTimeOffset.UtcNow;
        using var certificate = request.CreateSelfSigned(now.AddDays(-1), now.AddYears(10));
        var pfx = certificate.Export(X509ContentType.Pfx);
        File.WriteAllBytes(path, pfx);

        return X509CertificateLoader.LoadPkcs12(pfx, null);
    }

    public static byte[] Sha256Fingerprint(X509Certificate certificate) =>
        SHA256.HashData(certificate.GetRawCertData());
}
