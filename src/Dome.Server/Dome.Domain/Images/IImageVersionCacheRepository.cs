namespace Dome.Domain.Images;

public interface IImageVersionCacheRepository
{
    Task<IReadOnlyList<ImageVersionCache>> GetByReferencesAsync(
        IReadOnlyCollection<string> imageReferences,
        CancellationToken cancellationToken = default);

    Task UpsertAsync(ImageVersionCache entry, CancellationToken cancellationToken = default);
}
