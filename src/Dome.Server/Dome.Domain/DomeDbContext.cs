using Dome.Domain.Sockets;
using Microsoft.EntityFrameworkCore;

namespace Dome.Domain;

public sealed class DomeDbContext : DbContext
{
    public DomeDbContext(DbContextOptions<DomeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Socket> Sockets => Set<Socket>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DomeDbContext).Assembly);
    }
}
