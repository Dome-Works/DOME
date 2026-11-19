using Dome.Domain.Sockets;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Dome.Domain.Tests.Sockets;

public sealed class SocketRepositoryTests : IAsyncDisposable
{
    private readonly SqliteConnection _connection = new("DataSource=:memory:");

    public async ValueTask DisposeAsync() => await _connection.DisposeAsync();

    private async Task<DomeDbContext> CreateContextAsync()
    {
        if (_connection.State != System.Data.ConnectionState.Open)
        {
            await _connection.OpenAsync(TestContext.Current.CancellationToken);
        }

        var db = new DomeDbContext(new DbContextOptionsBuilder<DomeDbContext>().UseSqlite(_connection).Options);
        await db.Database.EnsureCreatedAsync(TestContext.Current.CancellationToken);
        return db;
    }

    [Fact]
    public async Task UpdateAsync_persists_changes_to_a_socket_loaded_by_ListAsync()
    {
        var ct = TestContext.Current.CancellationToken;
        var id = Guid.NewGuid();
        await using (var db = await CreateContextAsync())
        {
            await new SocketRepository(db).AddAsync(
                new Socket { Id = id, Name = "local", Address = "https://socket:8080", CreatedAt = DateTimeOffset.UtcNow },
                ct);
        }

        await using (var db = await CreateContextAsync())
        {
            var repository = new SocketRepository(db);
            var socket = (await repository.ListAsync(ct)).Single();
            socket.CertificateFingerprint = "pin";
            await repository.UpdateAsync(socket, ct);
        }

        await using (var db = await CreateContextAsync())
        {
            Assert.Equal("pin", (await new SocketRepository(db).GetByIdAsync(id, ct))!.CertificateFingerprint);
        }
    }

    [Theory]
    [InlineData("https://socket:8080", "protected-key", null, true)]
    [InlineData("https://moved:8080", "protected-key", null, false)]
    [InlineData("https://socket:8080", "rotated-key", null, false)]
    [InlineData("https://socket:8080", "protected-key", "existing", false)]
    public async Task TryPinCertificateAsync_pins_only_when_pairing_snapshot_still_matches(
        string currentAddress,
        string currentKey,
        string? currentPin,
        bool expectPinned)
    {
        var ct = TestContext.Current.CancellationToken;
        var id = Guid.NewGuid();
        await using (var db = await CreateContextAsync())
        {
            await new SocketRepository(db).AddAsync(
                new Socket
                {
                    Id = id,
                    Name = "local",
                    Address = currentAddress,
                    ProtectedApiKey = currentKey,
                    CertificateFingerprint = currentPin,
                    CreatedAt = DateTimeOffset.UtcNow
                },
                ct);
        }

        await using (var db = await CreateContextAsync())
        {
            var pinned = await new SocketRepository(db).TryPinCertificateAsync(
                id, "https://socket:8080", "protected-key", "new-pin", ct);
            Assert.Equal(expectPinned, pinned);
        }

        await using (var db = await CreateContextAsync())
        {
            var stored = (await new SocketRepository(db).GetByIdAsync(id, ct))!;
            Assert.Equal(expectPinned ? "new-pin" : currentPin, stored.CertificateFingerprint);
            Assert.Equal(currentAddress, stored.Address);
            Assert.Equal(currentKey, stored.ProtectedApiKey);
        }
    }
}
