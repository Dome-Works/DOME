using Dome.Domain.Seeding;
using Dome.Domain.Settings;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dome.Domain.Tests.Settings;

public sealed class SystemSettingRepositoryTests
{
    [Fact]
    public async Task Concurrent_SetAllIfEmpty_lets_exactly_one_writer_win()
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
                .AddInMemoryCollection(new Dictionary<string, string?> { ["ConnectionStrings:Dome"] = connectionString })
                .Build();
            var services = new ServiceCollection();
            services.AddDomeDomain(configuration);
            await using var provider = services.BuildServiceProvider();

            await using (var scope = provider.CreateAsyncScope())
                await scope.ServiceProvider.GetRequiredService<IDatabaseInitializer>().InitializeAsync(TestContext.Current.CancellationToken);

            var results = await Task.WhenAll(Enumerable.Range(0, 10).Select(i => Task.Run(async () =>
            {
                await using var scope = provider.CreateAsyncScope();
                return await scope.ServiceProvider.GetRequiredService<ISystemSettingRepository>().SetAllIfEmptyAsync(
                    "Guard",
                    new Dictionary<string, string> { ["Owner"] = i.ToString(), ["Guard"] = i.ToString() },
                    TestContext.Current.CancellationToken);
            })));

            Assert.Single(results, won => won);
            await using (var scope = provider.CreateAsyncScope())
            {
                var repository = scope.ServiceProvider.GetRequiredService<ISystemSettingRepository>();
                Assert.Equal(await repository.GetAsync("Guard", TestContext.Current.CancellationToken), await repository.GetAsync("Owner", TestContext.Current.CancellationToken));
            }
        }
        finally
        {
            SqliteConnection.ClearAllPools();
            if (Directory.Exists(root))
                Directory.Delete(root, recursive: true);
        }
    }
}
