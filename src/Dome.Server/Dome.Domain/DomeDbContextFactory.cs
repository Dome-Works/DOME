using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Dome.Domain;

public sealed class DomeDbContextFactory : IDesignTimeDbContextFactory<DomeDbContext>
{
    public DomeDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DomeDbContext>();
        optionsBuilder.UseSqlite("Data Source=design-time.db");
        return new DomeDbContext(optionsBuilder.Options);
    }
}
