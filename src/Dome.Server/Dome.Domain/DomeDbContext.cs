using Dome.Domain.Images;
using Dome.Domain.Settings;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Microsoft.EntityFrameworkCore;

namespace Dome.Domain;

public sealed class DomeDbContext : DbContext
{
    public DomeDbContext(DbContextOptions<DomeDbContext> options)
        : base(options)
    {
    }

    public DbSet<Socket> Sockets => Set<Socket>();

    public DbSet<Stack> Stacks => Set<Stack>();

    public DbSet<ImageVersionCache> ImageVersionCaches => Set<ImageVersionCache>();

    public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DomeDbContext).Assembly);
    }
}
