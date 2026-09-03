using Dome.Domain.Sqlite;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace Dome.Domain.Tests.Sqlite;

public sealed class SqlitePathTests
{
    [Fact]
    public void Uses_configured_connection_string()
    {
        const string configured = "Data Source=/var/lib/dome/dome.db";
        var configuration = ConfigurationWithConnectionString(configured);

        var resolved = SqlitePath.ResolveConnectionString(configuration);

        Assert.Equal(configured, resolved);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Empty_connection_string_uses_app_data(string? configured)
    {
        var configuration = configured is null
            ? new ConfigurationBuilder().Build()
            : ConfigurationWithConnectionString(configured);

        var resolved = SqlitePath.ResolveConnectionString(configuration);
        var dataSource = new SqliteConnectionStringBuilder(resolved).DataSource;

        var expected = Path.Join(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "Dome",
            "dome.db");

        Assert.Equal(Path.GetFullPath(expected), Path.GetFullPath(dataSource));
    }

    [Fact]
    public void Creates_database_folder()
    {
        var root = CreateTempRoot();
        try
        {
            var dbPath = Path.Join(root, "nested", "dome.db");
            var parent = Path.GetDirectoryName(dbPath)!;
            Assert.False(Directory.Exists(parent));

            var connectionString = new SqliteConnectionStringBuilder { DataSource = dbPath }.ConnectionString;
            SqlitePath.EnsureDataDirectory(connectionString);

            Assert.True(Directory.Exists(parent));
        }
        finally
        {
            Cleanup(root);
        }
    }

    private static IConfiguration ConfigurationWithConnectionString(string value)
    {
        return new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>
            {
                ["ConnectionStrings:Dome"] = value
            })
            .Build();
    }

    private static string CreateTempRoot()
    {
        var root = Path.Join(Path.GetTempPath(), "DomeTests", Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(root);
        return root;
    }

    private static void Cleanup(string root)
    {
        if (Directory.Exists(root))
        {
            Directory.Delete(root, recursive: true);
        }
    }
}
