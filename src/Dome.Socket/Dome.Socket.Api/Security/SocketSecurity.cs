using System.Security.Cryptography.X509Certificates;

namespace Dome.Socket.Api.Security;

public sealed record SocketSecurity(X509Certificate2 Certificate, byte[] CertificateSha256, byte[] Key);
