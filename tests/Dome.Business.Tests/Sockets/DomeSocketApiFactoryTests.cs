using System.Net;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Dome.Business.Sockets;
using Dome.Socket.Contracts.Pairing;
using Microsoft.AspNetCore.DataProtection;
using Refit;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Tests.Sockets;

public sealed class DomeSocketApiFactoryTests : IDisposable
{
    private const string ApiKey = "socket-key";

    private readonly SocketApiKeys _apiKeys = new(new EphemeralDataProtectionProvider());
    private readonly X509Certificate2 _certificate = CreateCertificate();

    public void Dispose() => _certificate.Dispose();

    [Fact]
    public async Task PairAsync_returns_fingerprint_for_valid_proof()
    {
        HttpRequestMessage? request = null;
        var factory = CreateFactory(ApiKey, r => request = r);

        var fingerprint = await factory.PairAsync(CreateSocket(null), TestContext.Current.CancellationToken);

        Assert.Equal(Fingerprint(_certificate), fingerprint);
        Assert.Null(request!.Headers.Authorization);
    }

    [Fact]
    public async Task PairAsync_returns_null_for_wrong_proof()
    {
        var factory = CreateFactory("other-key");

        var fingerprint = await factory.PairAsync(CreateSocket(null), TestContext.Current.CancellationToken);

        Assert.Null(fingerprint);
    }

    [Fact]
    public async Task Create_sends_bearer_key_when_certificate_matches_pin()
    {
        HttpRequestMessage? request = null;
        var factory = CreateFactory(ApiKey, r => request = r);

        await factory.Create(CreateSocket(Fingerprint(_certificate))).PairAsync(new PairRequest { Nonce = "AA==" }, TestContext.Current.CancellationToken);

        Assert.Equal("Bearer", request!.Headers.Authorization!.Scheme);
        Assert.Equal(ApiKey, request.Headers.Authorization.Parameter);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("0000")]
    public async Task Create_rejects_certificate_that_does_not_match_pin(string? pin)
    {
        var factory = CreateFactory(ApiKey);

        await Assert.ThrowsAsync<ApiRequestException>(
            () => factory.Create(CreateSocket(pin)).PairAsync(new PairRequest { Nonce = "AA==" }, TestContext.Current.CancellationToken));
    }

    [Fact]
    public void Create_returns_new_client_after_pin_changes()
    {
        var factory = CreateFactory(ApiKey);
        var socket = CreateSocket(null);

        var first = factory.Create(socket);
        var cached = factory.Create(socket);
        socket.CertificateFingerprint = Fingerprint(_certificate);
        var changed = factory.Create(socket);

        Assert.Same(first, cached);
        Assert.NotSame(first, changed);
    }

    private DomeSocketApiFactory CreateFactory(string socketKey, Action<HttpRequestMessage>? onRequest = null)
    {
        return new DomeSocketApiFactory(
            _apiKeys,
            validate => new FakeSocketHandler(validate, _certificate, socketKey, onRequest));
    }

    private SocketEntity CreateSocket(string? pin)
        => new()
        {
            Id = Guid.NewGuid(),
            Name = "socket",
            Address = "https://socket.local:8080",
            ProtectedApiKey = _apiKeys.Protect(ApiKey),
            CertificateFingerprint = pin
        };

    private static string Fingerprint(X509Certificate2 certificate)
        => Convert.ToHexStringLower(SHA256.HashData(certificate.RawData));

    private static X509Certificate2 CreateCertificate()
    {
        using var key = ECDsa.Create(ECCurve.NamedCurves.nistP256);
        var request = new CertificateRequest("CN=socket", key, HashAlgorithmName.SHA256);
        return request.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(1));
    }

    private sealed class FakeSocketHandler(
        RemoteCertificateValidationCallback validate,
        X509Certificate2 certificate,
        string socketKey,
        Action<HttpRequestMessage>? onRequest) : HttpMessageHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (!validate(this, certificate, null, SslPolicyErrors.RemoteCertificateChainErrors))
            {
                throw new HttpRequestException("Certificate rejected.");
            }

            onRequest?.Invoke(request);
            var pairRequest = await request.Content!.ReadFromJsonAsync<PairRequest>(cancellationToken);
            var proof = PairingProof.Compute(
                Encoding.UTF8.GetBytes(socketKey),
                Convert.FromBase64String(pairRequest!.Nonce),
                SHA256.HashData(certificate.RawData));

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = JsonContent.Create(new PairResponse { Proof = Convert.ToBase64String(proof) })
            };
        }
    }
}
