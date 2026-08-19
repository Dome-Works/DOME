using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace HomelabDocs.Domain.Sqlite;

internal static class SqlitePath
{
    public const string ConnectionStringName = "HomelabDocs";
    private const string DatabaseFileName = "homelabdocs.db";
    private const string ApplicationFolderName = "HomelabDocs";

    public static string ResolveConnectionString(IConfiguration configuration)
    {
        var configured = configuration.GetConnectionString(ConnectionStringName);
        if (!string.IsNullOrWhiteSpace(configured))
        {
            return configured;
        }

        var directory = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            ApplicationFolderName);

        return new SqliteConnectionStringBuilder
        {
            DataSource = Path.Join(directory, DatabaseFileName)
        }.ConnectionString;
    }

    public static void EnsureDataDirectory(string connectionString)
    {
        var dataSource = new SqliteConnectionStringBuilder(connectionString).DataSource;
        if (string.IsNullOrWhiteSpace(dataSource))
        {
            throw new InvalidOperationException(
                "ConnectionStrings:HomelabDocs must include a Data Source path.");
        }

        var fullPath = Path.GetFullPath(dataSource);
        var directory = Path.GetDirectoryName(fullPath);
        if (!string.IsNullOrEmpty(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}
