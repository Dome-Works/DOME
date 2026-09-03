using Microsoft.EntityFrameworkCore;

namespace Dome.Domain.Seeding;

internal sealed class DatabaseInitializer : IDatabaseInitializer
{
    private readonly DomeDbContext _db;

    public DatabaseInitializer(DomeDbContext db)
    {
        _db = db;
    }

    public Task InitializeAsync(CancellationToken cancellationToken = default)
        => _db.Database.MigrateAsync(cancellationToken);
}
