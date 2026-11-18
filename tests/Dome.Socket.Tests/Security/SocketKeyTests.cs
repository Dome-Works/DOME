using Dome.Socket.Api.Security;

namespace Dome.Socket.Tests.Security;

public sealed class SocketKeyTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N"));

    public void Dispose()
    {
        if (Directory.Exists(_directory))
        {
            Directory.Delete(_directory, true);
        }
    }

    [Fact]
    public void LoadOrCreate_generates_and_keeps_a_base64url_key()
    {
        var first = SocketKey.LoadOrCreate(_directory, null);
        var second = SocketKey.LoadOrCreate(_directory, null);

        Assert.Equal(43, first.Value.Length);
        Assert.Matches("^[A-Za-z0-9_-]+$", first.Value);
        Assert.Equal(first.Value, second.Value);
        Assert.False(first.IsConfigured);
        Assert.Equal(first.Value, File.ReadAllText(Path.Combine(_directory, SocketKey.KeyFileName)));
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void LoadOrCreate_ignores_blank_configured_key(string? configured)
    {
        Assert.False(SocketKey.LoadOrCreate(_directory, configured).IsConfigured);
    }

    [Fact]
    public void LoadOrCreate_prefers_configured_key()
    {
        var key = SocketKey.LoadOrCreate(_directory, " configured ");

        Assert.Equal("configured", key.Value);
        Assert.True(key.IsConfigured);
    }

    [Fact]
    public void MarkPaired_persists_and_returns_true_only_once()
    {
        var key = SocketKey.LoadOrCreate(_directory, null);
        Assert.False(key.IsPaired);

        Assert.True(key.MarkPaired());
        Assert.False(key.MarkPaired());
        Assert.True(SocketKey.LoadOrCreate(_directory, null).IsPaired);
    }

    [Fact]
    public void Changing_the_key_resets_pairing()
    {
        SocketKey.LoadOrCreate(_directory, "old").MarkPaired();

        Assert.False(SocketKey.LoadOrCreate(_directory, "new").IsPaired);
    }
}
