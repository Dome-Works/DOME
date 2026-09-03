using Dome.Business.Sockets;

namespace Dome.Business.Tests.Sockets;

public sealed class SocketAddressValidatorTests
{
    [Theory]
    [InlineData("http://socket:8080")]
    [InlineData("https://socket.local:8443")]
    [InlineData("HTTP://127.0.0.1:5110")]
    public void TryValidate_accepts_http_and_https(string address)
    {
        Assert.True(SocketAddressValidator.TryValidate(address, out var error));
        Assert.Equal(string.Empty, error);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void TryValidate_rejects_missing_address(string? address)
    {
        Assert.False(SocketAddressValidator.TryValidate(address, out var error));
        Assert.Equal("Address is required.", error);
    }

    [Theory]
    [InlineData("unix:///var/run/docker.sock")]
    [InlineData("tcp://192.168.1.10:2375")]
    [InlineData("not-a-uri")]
    public void TryValidate_rejects_non_http_addresses(string address)
    {
        Assert.False(SocketAddressValidator.TryValidate(address, out var error));
        Assert.False(string.IsNullOrWhiteSpace(error));
    }
}
