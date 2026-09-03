using Docker.DotNet.Models;
using Dome.Socket.Contracts.Containers;

namespace Dome.Socket.Api.Docker.Clients;

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
            Volumes = MapVolumes(container.Mounts),
        };
    }

    public static IReadOnlyCollection<ContainerVolumeResponse> MapVolumes(
        IList<MountPoint>? mounts)
    {
        if (mounts is null || mounts.Count == 0)
        {
            return Array.Empty<ContainerVolumeResponse>();
        }

        return mounts
            .Where(static mount => mount is not null)
            .Select(static mount => new ContainerVolumeResponse
            {
                Name = string.IsNullOrWhiteSpace(mount.Name) ? null : mount.Name.Trim(),
                Source = string.IsNullOrWhiteSpace(mount.Source) ? null : mount.Source.Trim(),
                Destination = string.IsNullOrWhiteSpace(mount.Destination)
                    ? string.Empty
                    : mount.Destination.Trim(),
                Type = string.IsNullOrWhiteSpace(mount.Type) ? null : mount.Type.Trim(),
                ReadOnly = !mount.RW,
            })
            .Where(static volume => volume.Destination.Length > 0)
            .ToArray();
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
