namespace Dome.Domain.Settings;

public interface ISystemSettingRepository
{
    Task<string?> GetAsync(string key, CancellationToken cancellationToken = default);

    Task SetAsync(string key, string value, CancellationToken cancellationToken = default);

    Task<bool> SetAllIfEmptyAsync(string guardKey, IReadOnlyDictionary<string, string> values, CancellationToken cancellationToken = default);
}
