using Microsoft.EntityFrameworkCore;

namespace Dome.Domain.Settings;

internal sealed class SystemSettingRepository : ISystemSettingRepository
{
    private readonly DomeDbContext _db;

    public SystemSettingRepository(DomeDbContext db)
    {
        _db = db;
    }

    public async Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);

        return await _db.SystemSettings
            .AsNoTracking()
            .Where(setting => setting.Key == key)
            .Select(setting => setting.Value)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task SetAsync(string key, string value, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(key);
        ArgumentNullException.ThrowIfNull(value);

        var existing = await _db.SystemSettings.FindAsync([key], cancellationToken);
        if (existing is null)
        {
            _db.SystemSettings.Add(new SystemSetting { Key = key, Value = value });
        }
        else
        {
            existing.Value = value;
        }

        await _db.SaveChangesAsync(cancellationToken);
    }
}
