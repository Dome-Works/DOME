using Microsoft.EntityFrameworkCore;

namespace HomelabDocs.Domain.Seeding;

internal sealed class DatabaseInitializer : IDatabaseInitializer
{
    private readonly HomelabDocsDbContext _db;

    public DatabaseInitializer(HomelabDocsDbContext db)
    {
        _db = db;
    }

    public Task InitializeAsync(CancellationToken cancellationToken = default)
        => _db.Database.MigrateAsync(cancellationToken);
}
