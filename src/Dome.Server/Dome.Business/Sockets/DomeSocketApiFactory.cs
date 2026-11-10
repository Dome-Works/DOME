using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Pairing;
using Refit;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Sockets;

public sealed class DomeSocketApiFactory : IDomeSocketApiFactory
{
    private static readonly RefitSettings RefitSettings = new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(
            new JsonSerializerOptions(JsonSerializerDefaults.Web))
    };

    private static readonly TimeSpan Timeout = TimeSpan.FromMinutes(15);

    private readonly ISocketApiKeys _apiKeys;
    private readonly Func<RemoteCertificateValidationCallback, HttpMessageHandler> _createHandler;
    private readonly ConcurrentDictionary<Guid, (ClientKey Key, IDomeSocketApi Api)> _clients = new();

    public DomeSocketApiFactory(ISocketApiKeys apiKeys)
        : this(apiKeys, CreateHandler)
    {
    }

    internal DomeSocketApiFactory(
        ISocketApiKeys apiKeys,
        Func<RemoteCertificateValidationCallback, HttpMessageHandler> createHandler)
    {
        _apiKeys = apiKeys;
        _createHandler = createHandler;
    }

    public IDomeSocketApi Create(SocketEntity socket)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socket.Address);

        var key = new ClientKey(socket.Address, socket.CertificateFingerprint, socket.ProtectedApiKey);
        if (_clients.TryGetValue(socket.Id, out var cached) && cached.Key == key)
        {
            return cached.Api;
        }

        var pin = socket.CertificateFingerprint;
        var client = CreateClient(
            socket.Address,
            (_, certificate, _, _) => certificate is not null && IsPinnedCertificate(pin, certificate.GetRawCertData()));

        var apiKey = _apiKeys.Unprotect(socket.ProtectedApiKey);
        if (apiKey is not null)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
        }

        var api = RestService.For<IDomeSocketApi>(client, RefitSettings);
        _clients[socket.Id] = (key, api);
        return api;
    }

    public async Task<string?> PairAsync(SocketEntity socket, CancellationToken cancellationToken)
    {
        var apiKey = _apiKeys.Unprotect(socket.ProtectedApiKey);
        if (apiKey is null || string.IsNullOrWhiteSpace(socket.Address))
        {
            return null;
        }

        byte[]? certificateSha256 = null;
        try
        {
            using var client = CreateClient(socket.Address, (_, certificate, _, _) =>
            {
                certificateSha256 = certificate is null ? null : SHA256.HashData(certificate.GetRawCertData());
                return certificate is not null;
            });

            var nonce = RandomNumberGenerator.GetBytes(32);
            var response = await RestService.For<IDomeSocketApi>(client, RefitSettings)
                .PairAsync(new PairRequest { Nonce = Convert.ToBase64String(nonce) }, cancellationToken);

            return IsValidProof(Encoding.UTF8.GetBytes(apiKey), nonce, certificateSha256, response.Proof)
                ? Convert.ToHexStringLower(certificateSha256!)
                : null;
        }
        catch (Exception ex) when (ex is not OperationCanceledException || !cancellationToken.IsCancellationRequested)
        {
            return null;
        }
    }

    internal static bool IsPinnedCertificate(string? pin, byte[] certificateDer)
    {
        return pin is not null
            && string.Equals(pin, Convert.ToHexStringLower(SHA256.HashData(certificateDer)), StringComparison.Ordinal);
    }

    internal static bool IsValidProof(byte[] key, byte[] nonce, byte[]? certificateSha256, string? proof)
    {
        if (certificateSha256 is null || string.IsNullOrEmpty(proof))
        {
            return false;
        }

        var proofBytes = new byte[32];
        return Convert.TryFromBase64String(proof, proofBytes, out var length)
            && length == proofBytes.Length
            && PairingProof.Verify(key, nonce, certificateSha256, proofBytes);
    }

    private HttpClient CreateClient(string address, RemoteCertificateValidationCallback validate)
    {
        return new HttpClient(_createHandler(validate))
        {
            BaseAddress = new Uri(address, UriKind.Absolute),
            Timeout = Timeout
        };
    }

    private static HttpMessageHandler CreateHandler(RemoteCertificateValidationCallback validate)
    {
        return new SocketsHttpHandler
        {
            SslOptions = new SslClientAuthenticationOptions { RemoteCertificateValidationCallback = validate }
        };
    }

    private sealed record ClientKey(string Address, string? CertificateFingerprint, string ProtectedApiKey);
}
