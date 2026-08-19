using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HomelabDocs.Domain;

public sealed class HomelabDocsDbContextFactory : IDesignTimeDbContextFactory<HomelabDocsDbContext>
{
    public HomelabDocsDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<HomelabDocsDbContext>();
        optionsBuilder.UseSqlite("Data Source=design-time.db");
        return new HomelabDocsDbContext(optionsBuilder.Options);
    }
}
