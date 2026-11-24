using System.Security.Cryptography;

namespace Dome.Socket.Contracts.Pairing;

public static class PairingProof
{
    public static byte[] Compute(byte[] key, byte[] nonce, byte[] certificateSha256)
    {
        return HMACSHA256.HashData(key, nonce.Concat(certificateSha256).ToArray());
    }

    public static bool Verify(byte[] key, byte[] nonce, byte[] certificateSha256, byte[] proof)
    {
        return CryptographicOperations.FixedTimeEquals(Compute(key, nonce, certificateSha256), proof);
    }
}
