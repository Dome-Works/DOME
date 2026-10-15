using Dome.Shared.Images;
using Dome.Socket.Contracts.Images;

namespace Dome.Business.Images;

public static class ImageVersionClassifier
{
    public static ImageVersionClassification Classify(
        string image,
        string? localDigest,
        ImageVersionSnapshot snapshot)
    {
        ArgumentNullException.ThrowIfNull(snapshot);

        var parsed = ImageReferenceParser.TryParse(image);
        var currentTag = parsed?.Tag;
        if (snapshot.Unsupported || HasNoRemoteData(snapshot))
        {
            return Unknown(currentTag);
        }

        var currentVersion = ImageTagParser.TryParse(currentTag);
        if (currentVersion is not null)
        {
            var newest = snapshot.Tags
                .Select(ImageTagParser.TryParse)
                .Where(tag => tag is not null && tag.Suffix == currentVersion.Suffix)
                .Select(tag => tag!)
                .OrderByDescending(tag => tag, Comparer<ImageTagVersion>.Create(ImageTagParser.Compare))
                .FirstOrDefault();

            if (newest is not null && ImageTagParser.Compare(newest, currentVersion) > 0)
            {
                var kind = newest.Major > currentVersion.Major
                    ? ImageVersionKind.Major
                    : newest.Minor > currentVersion.Minor
                        ? ImageVersionKind.Minor
                        : ImageVersionKind.Patch;
                return new ImageVersionClassification
                {
                    Kind = kind,
                    CurrentTag = currentTag,
                    NewestTag = newest.Original,
                };
            }
        }

        if (!string.IsNullOrWhiteSpace(localDigest)
            && !string.IsNullOrWhiteSpace(snapshot.RemoteDigest)
            && !localDigest.Equals(snapshot.RemoteDigest, StringComparison.OrdinalIgnoreCase))
        {
            return new ImageVersionClassification
            {
                Kind = ImageVersionKind.Digest,
                CurrentTag = currentTag,
                NewestTag = currentTag,
            };
        }

        if (currentVersion is not null
            || (!string.IsNullOrWhiteSpace(localDigest)
                && !string.IsNullOrWhiteSpace(snapshot.RemoteDigest)))
        {
            return new ImageVersionClassification
            {
                Kind = ImageVersionKind.None,
                CurrentTag = currentTag,
                NewestTag = currentTag,
            };
        }

        return Unknown(currentTag);
    }

    private static bool HasNoRemoteData(ImageVersionSnapshot snapshot)
        => string.IsNullOrWhiteSpace(snapshot.RemoteDigest)
           && snapshot.Tags.Count == 0;

    private static ImageVersionClassification Unknown(string? currentTag)
        => new()
        {
            Kind = ImageVersionKind.Unknown,
            CurrentTag = currentTag,
        };
}
