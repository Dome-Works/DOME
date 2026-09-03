using HomelabDocs.Domain.Sockets;
using Microsoft.EntityFrameworkCore;

namespace HomelabDocs.Domain;

public sealed class HomelabDocsDbContext : DbContext
{
    public HomelabDocsDbContext(DbContextOptions<HomelabDocsDbContext> options)
        : base(options)
    {
    }

    public DbSet<Socket> Sockets => Set<Socket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HomelabDocsDbContext).Assembly);
    }
}
