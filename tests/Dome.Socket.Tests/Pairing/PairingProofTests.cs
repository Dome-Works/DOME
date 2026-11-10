using Dome.Socket.Contracts.Pairing;

namespace Dome.Socket.Tests.Pairing;

public sealed class PairingProofTests
{
    private static readonly byte[] Key = Enumerable.Range(0, 32).Select(i => (byte)i).ToArray();
    private static readonly byte[] Nonce = Enumerable.Repeat((byte)0xAA, 16).ToArray();
    private static readonly byte[] Fingerprint = Enumerable.Repeat((byte)0x01, 32).ToArray();

    [Fact]
    public void Compute_matches_known_vector()
    {
        var proof = PairingProof.Compute(Key, Nonce, Fingerprint);

        Assert.Equal("bcbe36359af4f7137dc1cf446e1c8898ef581b4b1941aaa0315e483877a7e749", Convert.ToHexStringLower(proof));
    }

    [Fact]
    public void Verify_returns_true_for_matching_proof()
    {
        var proof = PairingProof.Compute(Key, Nonce, Fingerprint);

        Assert.True(PairingProof.Verify(Key, Nonce, Fingerprint, proof));
    }

    [Fact]
    public void Verify_returns_false_for_different_fingerprint()
    {
        var proof = PairingProof.Compute(Key, Nonce, Fingerprint);
        var otherFingerprint = Enumerable.Repeat((byte)0x02, 32).ToArray();

        Assert.False(PairingProof.Verify(Key, Nonce, otherFingerprint, proof));
    }

    [Fact]
    public void Verify_returns_false_for_different_key()
    {
        var proof = PairingProof.Compute(Key, Nonce, Fingerprint);
        var otherKey = Enumerable.Repeat((byte)0xFF, 32).ToArray();

        Assert.False(PairingProof.Verify(otherKey, Nonce, Fingerprint, proof));
    }
}
