using Dome.Business.Auth;

namespace Dome.Business.Tests.Auth;

public sealed class PasswordHasherTests
{
    [Fact]
    public void Verify_returns_true_for_correct_password()
    {
        var encoded = PasswordHasher.Hash("correct horse");

        Assert.True(PasswordHasher.Verify("correct horse", encoded));
    }

    [Fact]
    public void Verify_returns_false_for_wrong_password()
    {
        var encoded = PasswordHasher.Hash("correct horse");

        Assert.False(PasswordHasher.Verify("wrong horse", encoded));
    }

    [Fact]
    public void Verify_returns_false_for_tampered_hash()
    {
        var parts = PasswordHasher.Hash("correct horse").Split('$');
        var hash = Convert.FromBase64String(parts[3]);
        hash[0] ^= 0xFF;
        parts[3] = Convert.ToBase64String(hash);

        Assert.False(PasswordHasher.Verify("correct horse", string.Join('$', parts)));
    }

    [Theory]
    [InlineData("")]
    [InlineData("garbage")]
    [InlineData("v2$600000$AAAA$AAAA")]
    [InlineData("v1$abc$AAAA$AAAA")]
    [InlineData("v1$-1$AAAA$AAAA")]
    [InlineData("v1$1$not-base64!$AAAA")]
    [InlineData("v1$1$AAAA$")]
    public void Verify_returns_false_for_malformed_input(string encoded)
    {
        Assert.False(PasswordHasher.Verify("password", encoded));
    }

    [Fact]
    public void Hash_produces_different_output_for_same_password()
    {
        Assert.NotEqual(PasswordHasher.Hash("same"), PasswordHasher.Hash("same"));
    }
}
