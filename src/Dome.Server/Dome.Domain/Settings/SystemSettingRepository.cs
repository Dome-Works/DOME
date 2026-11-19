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

        await StageAsync(key, value, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> SetAllIfEmptyAsync(string guardKey, IReadOnlyDictionary<string, string> values, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(guardKey);
        ArgumentNullException.ThrowIfNull(values);
        foreach (var (key, value) in values)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(key);
            ArgumentNullException.ThrowIfNull(value);
        }

        await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);

        var guard = await _db.SystemSettings.FindAsync([guardKey], cancellationToken);
        if (!string.IsNullOrEmpty(guard?.Value))
            return false;

        foreach (var (key, value) in values)
            await StageAsync(key, value, cancellationToken);

        await _db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return true;
    }

    private async Task StageAsync(string key, string value, CancellationToken cancellationToken)
    {
        var existing = await _db.SystemSettings.FindAsync([key], cancellationToken);
        if (existing is null)
        {
            _db.SystemSettings.Add(new SystemSetting { Key = key, Value = value });
        }
        else
        {
            existing.Value = value;
        }
    }
}
