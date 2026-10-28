using Dome.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace Dome.Domain.Seeding;

internal sealed class DatabaseInitializer : IDatabaseInitializer
{
    private readonly DomeDbContext _db;

    public DatabaseInitializer(DomeDbContext db)
    {
        _db = db;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await _db.Database.MigrateAsync(cancellationToken);

        var defaults = new Dictionary<string, string>
        {
            [SystemSettingKeys.TelemetryEnabled] = bool.FalseString,
            [SystemSettingKeys.InstallationId] = Guid.NewGuid().ToString(),
            [SystemSettingKeys.OnboardingCompleted] = bool.FalseString,
        };
        var existing = await _db.SystemSettings
            .Where(setting => defaults.Keys.Contains(setting.Key))
            .Select(setting => setting.Key)
            .ToListAsync(cancellationToken);

        // Only add missing rows so user-chosen values survive restarts.
        foreach (var (key, value) in defaults.Where(pair => !existing.Contains(pair.Key)))
        {
            _db.SystemSettings.Add(new SystemSetting { Key = key, Value = value });
        }

        await _db.SaveChangesAsync(cancellationToken);
    }
}
