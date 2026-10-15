using Dome.Shared.Images;

namespace Dome.Business.Images;

public interface IDeviceImageVersionChecker
{
    Task<IReadOnlyCollection<ImageVersionCheckDto>?> GetAsync(
        string deviceName,
        CancellationToken cancellationToken = default);
}
