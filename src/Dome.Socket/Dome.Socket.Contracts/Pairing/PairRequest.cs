namespace Dome.Socket.Contracts.Pairing;

public sealed record PairRequest
{
    public required string Nonce { get; init; }
}
