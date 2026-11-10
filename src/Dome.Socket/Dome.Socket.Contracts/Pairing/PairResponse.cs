namespace Dome.Socket.Contracts.Pairing;

public sealed record PairResponse
{
    public required string Proof { get; init; }
}
