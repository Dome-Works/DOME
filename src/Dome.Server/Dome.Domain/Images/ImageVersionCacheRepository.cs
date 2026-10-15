using Microsoft.EntityFrameworkCore;
using Dome.Domain;

namespace Dome.Domain.Images;

internal sealed class ImageVersionCacheRepository : IImageVersionCacheRepository
{
    private readonly DomeDbContext _db;

    public ImageVersionCacheRepository(DomeDbContext db)
    {
        _db = db;
    }

    public async Task<IReadOnlyList<ImageVersionCache>> GetByReferencesAsync(
        IReadOnlyCollection<string> imageReferences,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(imageReferences);
        if (imageReferences.Count == 0)
        {
            return Array.Empty<ImageVersionCache>();
        }

        var keys = imageReferences
            .Where(static value => !string.IsNullOrWhiteSpace(value))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
        if (keys.Length == 0)
        {
            return Array.Empty<ImageVersionCache>();
        }

        return await _db.ImageVersionCaches
            .AsNoTracking()
            .Where(entry => keys.Contains(entry.ImageReference))
            .ToListAsync(cancellationToken);
    }

    public async Task UpsertAsync(
        ImageVersionCache entry,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(entry);

        var existing = await _db.ImageVersionCaches
            .FirstOrDefaultAsync(
                cached => cached.ImageReference == entry.ImageReference,
                cancellationToken);
        if (existing is null)
        {
            if (entry.Id == Guid.Empty)
            {
                entry.Id = Guid.NewGuid();
            }

            _db.ImageVersionCaches.Add(entry);
        }
        else
        {
            existing.RemoteDigest = entry.RemoteDigest;
            existing.RemoteTags = entry.RemoteTags;
            existing.Unsupported = entry.Unsupported;
            existing.LastError = entry.LastError;
            existing.CheckedAt = entry.CheckedAt;
        }

        await _db.SaveChangesAsync(cancellationToken);
    }
}
