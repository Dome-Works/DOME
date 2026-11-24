using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Dome.Domain.Sqlite;

public static class DomeDataDirectory
{
    public static string Resolve(IConfiguration configuration)
    {
        var dataSource = new SqliteConnectionStringBuilder(SqlitePath.ResolveConnectionString(configuration)).DataSource;
        return Path.GetDirectoryName(Path.GetFullPath(dataSource))!;
    }
}
