using FastEndpoints;
using Dome.Socket.Api.Security;
using Dome.Socket.Contracts.Pairing;

namespace Dome.Socket.Api.Endpoints.Pairing;

public sealed class PairEndpoint : Endpoint<PairRequest, PairResponse>
{
    private readonly SocketSecurity _security;

    public PairEndpoint(SocketSecurity security)
    {
        _security = security;
    }

    public override void Configure()
    {
        Post("/api/pair");
        AllowAnonymous();
    }

    public override async Task HandleAsync(PairRequest req, CancellationToken ct)
    {
        var nonce = new byte[64];
        if (!Convert.TryFromBase64String(req.Nonce, nonce, out var length) || length < 16)
        {
            await Send.ResultAsync(TypedResults.BadRequest());
            return;
        }

        if (_security.Key.Length == 0)
        {
            await Send.UnauthorizedAsync(ct);
            return;
        }

        var proof = PairingProof.Compute(_security.Key, nonce[..length], _security.CertificateSha256);
        await Send.OkAsync(new PairResponse { Proof = Convert.ToBase64String(proof) }, ct);
    }
}
