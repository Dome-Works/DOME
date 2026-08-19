using Microsoft.EntityFrameworkCore;

namespace HomelabDocs.Domain;

public sealed class HomelabDocsDbContext : DbContext
{
    public HomelabDocsDbContext(DbContextOptions<HomelabDocsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomelabDocsDbContext).Assembly);
    }
}
