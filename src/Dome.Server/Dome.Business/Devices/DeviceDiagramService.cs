using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Shared.Containers;
using Dome.Shared.Diagrams;
using StackEntity = Dome.Domain.Stacks.Stack;

namespace Dome.Business.Devices;

public sealed class DeviceDiagramService : IDeviceDiagramService
{
    private readonly ISocketRepository _socketRepository;
    private readonly IStackRepository _stackRepository;
    private readonly IDeviceQueryService _deviceQueryService;

    public DeviceDiagramService(
        ISocketRepository socketRepository,
        IStackRepository stackRepository,
        IDeviceQueryService deviceQueryService)
    {
        _socketRepository = socketRepository;
        _stackRepository = stackRepository;
        _deviceQueryService = deviceQueryService;
    }

    public async Task<DeviceDiagramDto?> GetDiagramAsync(
        string socketName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(socketName);

        var socket = await _socketRepository.GetByNameAsync(socketName, cancellationToken);
        if (socket is null)
        {
            return null;
        }

        var persistedStacks = await _stackRepository.ListBySocketIdAsync(
            socket.Id,
            cancellationToken);
        var containers = await _deviceQueryService.GetContainersAsync(
            socketName,
            cancellationToken);
        if (containers is null)
        {
            return null;
        }

        var boundContainers = new List<ContainerDto>(containers.Count);
        var readonlyProjects = new SortedSet<string>(StringComparer.Ordinal);

        foreach (var container in containers)
        {
            var composeProject = NormalizeComposeProject(container.Stack);
            if (composeProject is null)
            {
                boundContainers.Add(container with { Stack = null });
                continue;
            }
            
            var managed = persistedStacks.FirstOrDefault(stack => string.Equals(stack.ProjectName, composeProject, StringComparison.OrdinalIgnoreCase));
            if (managed is not null)
            {
                boundContainers.Add(container with { Stack = managed.ProjectName });
                continue;
            }

            readonlyProjects.Add(composeProject);
            boundContainers.Add(container with { Stack = composeProject });
        }

        var containersByStack = boundContainers
            .Where(static c => !string.IsNullOrWhiteSpace(c.Stack))
            .GroupBy(static c => c.Stack!, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(
                static g => g.Key,
                static g => CalculateStackTotalBytes(g),
                StringComparer.OrdinalIgnoreCase);

        var stacks = new List<DiagramStackDto>(persistedStacks.Count + readonlyProjects.Count);
        foreach (var stack in persistedStacks.OrderBy(static item => item.ProjectName, StringComparer.Ordinal))
        {
            containersByStack.TryGetValue(stack.ProjectName, out var totalBytes);
            stacks.Add(
                new DiagramStackDto
                {
                    Id = stack.Id,
                    ProjectName = stack.ProjectName,
                    Kind = DiagramStackKind.Managed,
                    TotalBytes = totalBytes
                });
        }

        foreach (var projectName in readonlyProjects)
        {
            containersByStack.TryGetValue(projectName, out var totalBytes);
            stacks.Add(
                new DiagramStackDto
                {
                    Id = null,
                    ProjectName = projectName,
                    Kind = DiagramStackKind.ReadOnly,
                    TotalBytes = totalBytes
                });
        }

        return new DeviceDiagramDto
        {
            Stacks = stacks,
            Containers = boundContainers
        };
    }

    private static long CalculateStackTotalBytes(IEnumerable<ContainerDto> stackContainers)
    {
        var seenKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        long totalBytes = 0;

        foreach (var container in stackContainers)
        {
            foreach (var volume in container.Volumes)
            {
                var size = volume.SizeBytes ?? 0;
                if (size <= 0)
                {
                    continue;
                }

                var volumeKey = !string.IsNullOrWhiteSpace(volume.Name)
                    ? $"name:{volume.Name.Trim()}"
                    : (!string.IsNullOrWhiteSpace(volume.Source) ? $"source:{volume.Source.Trim()}" : null);

                if (volumeKey is not null)
                {
                    if (seenKeys.Add(volumeKey))
                    {
                        totalBytes += size;
                    }
                }
                else
                {
                    totalBytes += size;
                }
            }
        }

        return totalBytes;
    }

    private static string? NormalizeComposeProject(string? stack)
    {
        if (string.IsNullOrWhiteSpace(stack))
        {
            return null;
        }

        return stack.Trim();
    }
}
