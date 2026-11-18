using Dome.Business.Sockets;
using Microsoft.AspNetCore.DataProtection;

namespace Dome.Business.Tests.Sockets;

public sealed class SocketApiKeysTests
{
    private readonly SocketApiKeys _apiKeys = new(new EphemeralDataProtectionProvider());

    [Fact]
    public void Unprotect_returns_original_key()
    {
        const string key = "socket-key";

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
