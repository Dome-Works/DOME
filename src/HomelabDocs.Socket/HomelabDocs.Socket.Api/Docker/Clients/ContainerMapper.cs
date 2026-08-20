using Docker.DotNet.Models;
using HomelabDocs.Socket.Contracts.Containers;

namespace HomelabDocs.Socket.Api.Docker.Clients;

internal static class ContainerMapper
{
    private const int ShortIdLength = 12;
    private const string ComposeProjectLabel = "com.docker.compose.project";

    public static ContainerResponse Map(ContainerListResponse container)
    {
        ArgumentNullException.ThrowIfNull(container);

        var id = container.ID ?? string.Empty;
        var name = ResolveName(container.Names, id);

        return new ContainerResponse
        {
            Id = id,
            Name = name,
            State = container.State ?? string.Empty,
            Stack = ResolveStack(container.Labels),
        };
    }

    public static string? ResolveStack(IDictionary<string, string>? labels)
    {
        if (labels is null)
        {
            return null;
        }

        if (!labels.TryGetValue(ComposeProjectLabel, out var project)
            || string.IsNullOrWhiteSpace(project))
        {
            return null;
        }

        return project.Trim();
    }

    public static string ResolveName(IList<string>? names, string containerId)
    {
        if (names is not null)
        {
            foreach (var rawName in names)
            {
                if (string.IsNullOrWhiteSpace(rawName))
                {
                    continue;
                }

                return rawName.TrimStart('/');
            }
        }

        return ShortenId(containerId);
    }

    public static string ShortenId(string containerId)
    {
        if (string.IsNullOrWhiteSpace(containerId))
        {
            return string.Empty;
        }

        return containerId.Length <= ShortIdLength
            ? containerId
            : containerId[..ShortIdLength];
    }
}
