using HomelabDocs.Domain.Seeding;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomelabDocs.Domain.Tests.Seeding;

public sealed class DatabaseInitializerTests
{
    [Fact]
    public async Task Applying_migrations_twice_keeps_a_single_history_row()
    {
        var root = Path.Combine(Path.GetTempPath(), "HomelabDocsTests", Guid.NewGuid().ToString("N"));
        var dbPath = Path.Combine(root, "nested", "homelabdocs.db");
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = dbPath,
            Pooling = false
        }.ConnectionString;

        try
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:HomelabDocs"] = connectionString
                })
                .Build();

            var services = new ServiceCollection();
            services.AddHomelabDocsDomain(configuration);

            await using (var provider = services.BuildServiceProvider())
            {
                await InitializeAsync(provider);
                await InitializeAsync(provider);
            }

            Assert.True(File.Exists(dbPath));
            Assert.Equal(1, await CountAppliedMigrationsAsync(connectionString));
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(root))
            {
                Directory.Delete(root, recursive: true);
            }
        }
    }

    private static async Task InitializeAsync(IServiceProvider provider)
    {
        await using var scope = provider.CreateAsyncScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>();
        await initializer.InitializeAsync();
    }

    private static async Task<long> CountAppliedMigrationsAsync(string connectionString)
    {
        await using var connection = new SqliteConnection(connectionString);
        await connection.OpenAsync();
        await using var command = connection.CreateCommand();
        command.CommandText = """SELECT COUNT(*) FROM "__EFMigrationsHistory";""";
        var result = await command.ExecuteScalarAsync();
        return Convert.ToInt64(result);
    }
}
