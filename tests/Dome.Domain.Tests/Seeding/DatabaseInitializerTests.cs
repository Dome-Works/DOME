using Dome.Domain.Seeding;
using Dome.Domain.Settings;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dome.Domain.Tests.Seeding;

public sealed class DatabaseInitializerTests
{
    [Fact]
    public async Task Applying_migrations_twice_keeps_a_stable_history()
    {
        var root = Path.Join(Path.GetTempPath(), "DomeTests", Guid.NewGuid().ToString("N"));
        var dbPath = Path.Join(root, "nested", "dome.db");
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
                    ["ConnectionStrings:Dome"] = connectionString
                })
                .Build();

            var services = new ServiceCollection();
            services.AddDomeDomain(configuration);

            await using (var provider = services.BuildServiceProvider())
            {
                await InitializeAsync(provider);
                await InitializeAsync(provider);
            }

            Assert.True(File.Exists(dbPath));
            var applied = await CountAppliedMigrationsAsync(connectionString);
            Assert.True(applied >= 2);

            await using (var provider = services.BuildServiceProvider())
            {
                await InitializeAsync(provider);
            }

            Assert.Equal(applied, await CountAppliedMigrationsAsync(connectionString));
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

    [Fact]
    public async Task Initializing_seeds_system_settings_without_overwriting_them()
    {
        var root = Path.Join(Path.GetTempPath(), "DomeTests", Guid.NewGuid().ToString("N"));
        var connectionString = new SqliteConnectionStringBuilder
        {
            DataSource = Path.Join(root, "dome.db"),
            Pooling = false
        }.ConnectionString;

        try
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string?>
                {
                    ["ConnectionStrings:Dome"] = connectionString
                })
                .Build();

            var services = new ServiceCollection();
            services.AddDomeDomain(configuration);
            await using var provider = services.BuildServiceProvider();

            await InitializeAsync(provider);
            Assert.Equal(bool.FalseString, await GetSettingAsync(provider, SystemSettingKeys.TelemetryEnabled));
            var installationId = await GetSettingAsync(provider, SystemSettingKeys.InstallationId);
            Assert.True(Guid.TryParse(installationId, out _));

            await using (var scope = provider.CreateAsyncScope())
            {
                await scope.ServiceProvider.GetRequiredService<ISystemSettingRepository>()
                    .SetAsync(SystemSettingKeys.TelemetryEnabled, bool.TrueString, TestContext.Current.CancellationToken);
            }

            await InitializeAsync(provider);
            Assert.Equal(bool.TrueString, await GetSettingAsync(provider, SystemSettingKeys.TelemetryEnabled));
            Assert.Equal(installationId, await GetSettingAsync(provider, SystemSettingKeys.InstallationId));
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

    private static async Task<string?> GetSettingAsync(IServiceProvider provider, string key)
    {
        await using var scope = provider.CreateAsyncScope();
        return await scope.ServiceProvider.GetRequiredService<ISystemSettingRepository>().GetAsync(key);
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
