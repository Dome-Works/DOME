namespace Dome.Socket.Contracts.Images;

public static class ImageReferenceParser
{
    public const string DockerHubHost = "docker.io";

    private const string DefaultTag = "latest";
    private const string LibraryNamespace = "library";

    public static ImageReference? TryParse(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return null;
        }

        var remaining = value.Trim();
        string? digest = null;
        var digestSeparator = remaining.IndexOf('@');
        if (digestSeparator >= 0)
        {
            if (digestSeparator == 0 || digestSeparator == remaining.Length - 1)
            {
                return null;
            }

            digest = remaining[(digestSeparator + 1)..].Trim();
            remaining = remaining[..digestSeparator];
            if (digest.Length == 0)
            {
                return null;
            }
        }

        string? tag = null;
        var lastSlash = remaining.LastIndexOf('/');
        var lastColon = remaining.LastIndexOf(':');
        if (lastColon > lastSlash)
        {
            tag = remaining[(lastColon + 1)..];
            remaining = remaining[..lastColon];
            if (tag.Length == 0)
            {
                return null;
            }
        }

        if (remaining.Length == 0)
        {
            return null;
        }

        string host;
        string repository;
        var firstSlash = remaining.IndexOf('/');
        if (firstSlash < 0)
        {
            host = DockerHubHost;
            repository = $"{LibraryNamespace}/{remaining}";
        }
        else
        {
            var candidateHost = remaining[..firstSlash];
            if (IsRegistryHost(candidateHost))
            {
                host = NormalizeHost(candidateHost);
                repository = remaining[(firstSlash + 1)..];
                if (host == DockerHubHost && repository.IndexOf('/') < 0)
                {
                    repository = $"{LibraryNamespace}/{repository}";
                }
            }
            else
            {
                host = DockerHubHost;
                repository = remaining;
            }
        }

        if (repository.Length == 0)
        {
            return null;
        }

        tag ??= digest is null ? DefaultTag : null;

        return new ImageReference
        {
            Host = host,
            Repository = repository,
            Tag = tag,
            Digest = digest,
        };
    }

    private static bool IsRegistryHost(string value)
        => value.Equals("localhost", StringComparison.OrdinalIgnoreCase)
           || value.Contains('.', StringComparison.Ordinal)
           || value.Contains(':', StringComparison.Ordinal);

    private static string NormalizeHost(string host)
    {
        var normalized = host.Trim().ToLowerInvariant();
        return normalized is "index.docker.io" or "registry-1.docker.io"
            ? DockerHubHost
            : normalized;
    }
}
