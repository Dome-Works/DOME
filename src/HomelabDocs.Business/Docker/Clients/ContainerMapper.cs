using Docker.DotNet.Models;
using HomelabDocs.Shared.Containers;

namespace HomelabDocs.Business.Docker.Clients;

internal static class ContainerMapper
{
    private const int ShortIdLength = 12;

    public static ContainerResponse Map(ContainerListResponse container)
    {
        ArgumentNullException.ThrowIfNull(container);

        var id = container.ID ?? string.Empty;
        var name = ResolveName(container.Names, id);

        return new ContainerResponse
        {
            Id = id,
            Name = name,
            Image = container.Image ?? string.Empty,
            State = container.State ?? string.Empty,
            Status = container.Status ?? string.Empty,
            Ports = MapPorts(container.Ports)
        };
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

    public static IReadOnlyCollection<ContainerPortResponse> MapPorts(IList<Port>? ports)
    {
        if (ports is null || ports.Count == 0)
        {
            return Array.Empty<ContainerPortResponse>();
        }

        return ports
            .Select(port => new ContainerPortResponse
            {
                Type = port.Type ?? string.Empty,
                PrivatePort = port.PrivatePort,
                PublicPort = port.PublicPort == 0 ? null : port.PublicPort,
                IpAddress = string.IsNullOrWhiteSpace(port.IP) ? null : port.IP
            })
            .ToArray();
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
