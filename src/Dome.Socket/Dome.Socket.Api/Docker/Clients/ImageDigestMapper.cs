namespace Dome.Socket.Api.Docker.Clients;

internal static class ImageDigestMapper
{
    public static string? Resolve(string? image, IReadOnlyCollection<string>? repoDigests)
    {
        var embedded = ContainerMapper.ResolveDigest(image);
        if (embedded is not null)
        {
            return embedded;
        }

        if (repoDigests is null || repoDigests.Count == 0)
        {
            return null;
        }

        var repository = ResolveRepository(image);
        if (repository is not null)
        {
            foreach (var repoDigest in repoDigests)
            {
                if (!TrySplitRepoDigest(repoDigest, out var name, out var digest))
                {
                    continue;
                }

                if (RepositoriesMatch(repository, name))
                {
                    return digest;
                }
            }
        }

        foreach (var repoDigest in repoDigests)
        {
            if (TrySplitRepoDigest(repoDigest, out _, out var digest))
            {
                return digest;
            }
        }

        return null;
    }

    private static string? ResolveRepository(string? image)
    {
        if (string.IsNullOrWhiteSpace(image))
        {
            return null;
        }

        var value = image.Trim();
        var digestSeparator = value.IndexOf('@');
        if (digestSeparator >= 0)
        {
            value = value[..digestSeparator];
        }

        var lastSlash = value.LastIndexOf('/');
        var tagSeparator = value.LastIndexOf(':');
        if (tagSeparator > lastSlash)
        {
            value = value[..tagSeparator];
        }

        return value.Length == 0 ? null : value;
    }

    private static bool TrySplitRepoDigest(string repoDigest, out string name, out string digest)
    {
        name = string.Empty;
        digest = string.Empty;
        if (string.IsNullOrWhiteSpace(repoDigest))
        {
            return false;
        }

        var separator = repoDigest.LastIndexOf('@');
        if (separator <= 0 || separator == repoDigest.Length - 1)
        {
            return false;
        }

        name = repoDigest[..separator].Trim();
        digest = repoDigest[(separator + 1)..].Trim();
        return name.Length > 0 && digest.Length > 0;
    }

    private static bool RepositoriesMatch(string imageRepository, string digestName)
    {
        return imageRepository.Equals(digestName, StringComparison.OrdinalIgnoreCase)
               || digestName.EndsWith('/' + imageRepository, StringComparison.OrdinalIgnoreCase)
               || imageRepository.EndsWith('/' + digestName, StringComparison.OrdinalIgnoreCase);
    }
}
