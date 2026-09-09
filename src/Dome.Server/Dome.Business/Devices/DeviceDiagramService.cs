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

            var managed = FindManagedStack(persistedStacks, composeProject);
            if (managed is not null)
            {
                boundContainers.Add(container with { Stack = managed.ProjectName });
                continue;
            }

            readonlyProjects.Add(composeProject);
            boundContainers.Add(container with { Stack = composeProject });
        }

        var stacks = new List<DiagramStackDto>(persistedStacks.Count + readonlyProjects.Count);
        foreach (var stack in persistedStacks.OrderBy(static item => item.ProjectName, StringComparer.Ordinal))
        {
            stacks.Add(
                new DiagramStackDto
                {
                    Id = stack.Id,
                    ProjectName = stack.ProjectName,
                    Kind = DiagramStackKind.Managed
                });
        }

        foreach (var projectName in readonlyProjects)
        {
            stacks.Add(
                new DiagramStackDto
                {
                    Id = null,
                    ProjectName = projectName,
                    Kind = DiagramStackKind.ReadOnly
                });
        }

        return new DeviceDiagramDto
        {
            Stacks = stacks,
            Containers = boundContainers
        };
    }

    private static string? NormalizeComposeProject(string? stack)
    {
        if (string.IsNullOrWhiteSpace(stack))
        {
            return null;
        }

        return stack.Trim();
    }

    private static StackEntity? FindManagedStack(
        IReadOnlyList<StackEntity> persistedStacks,
        string composeProject)
    {
        foreach (var stack in persistedStacks)
        {
            if (string.Equals(stack.ProjectName, composeProject, StringComparison.OrdinalIgnoreCase))
            {
                return stack;
            }
        }

        return null;
    }
}
