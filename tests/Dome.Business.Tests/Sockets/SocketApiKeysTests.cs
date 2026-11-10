using Dome.Business.Sockets;
using Microsoft.AspNetCore.DataProtection;

namespace Dome.Business.Tests.Sockets;

public sealed class SocketApiKeysTests
{
    private readonly SocketApiKeys _apiKeys = new(new EphemeralDataProtectionProvider());

    [Fact]
    public void Generate_returns_unique_base64url_keys_of_32_bytes()
    {
        var first = _apiKeys.Generate();
        var second = _apiKeys.Generate();

        Assert.Equal(43, first.Length);
        Assert.Matches("^[A-Za-z0-9_-]+$", first);
        Assert.NotEqual(first, second);
    }

    [Fact]
    public void Unprotect_returns_original_key()
    {
        var key = _apiKeys.Generate();

        var protectedKey = _apiKeys.Protect(key);

        Assert.NotEqual(key, protectedKey);
        Assert.Equal(key, _apiKeys.Unprotect(protectedKey));
    }

    [Theory]
    [InlineData("")]
    [InlineData("garbage")]
    public void Unprotect_returns_null_for_invalid_input(string protectedKey)
    {
        Assert.Null(_apiKeys.Unprotect(protectedKey));
    }
}
