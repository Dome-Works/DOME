using FakeItEasy;
using Dome.Business.Devices;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Shared.Containers;
using Dome.Shared.Diagrams;
using SocketEntity = Dome.Domain.Sockets.Socket;
using StackEntity = Dome.Domain.Stacks.Stack;

namespace Dome.Business.Tests.Devices;

public sealed class DeviceDiagramServiceTests
{
    [Fact]
    public async Task GetDiagramAsync_returns_null_when_device_is_missing()
    {
        var service = CreateService(socket: null, [], []);

        var result = await service.GetDiagramAsync("missing");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetDiagramAsync_binds_containers_to_managed_stacks()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "app");
        var containers = new[]
        {
            CreateContainer("c1", "web", "app"),
        };

        var service = CreateService(socket, [stack], containers);

        var result = await service.GetDiagramAsync("local");

        Assert.NotNull(result);
        var diagramStack = Assert.Single(result.Stacks);
        Assert.Equal(stack.Id, diagramStack.Id);
        Assert.Equal("app", diagramStack.ProjectName);
        Assert.Equal(DiagramStackKind.Managed, diagramStack.Kind);
        var container = Assert.Single(result.Containers);
        Assert.Equal("app", container.Stack);
    }

    [Fact]
    public async Task GetDiagramAsync_binds_compose_project_case_insensitively()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "app");
        var containers = new[]
        {
            CreateContainer("c1", "web", "APP"),
        };

        var service = CreateService(socket, [stack], containers);

        var result = await service.GetDiagramAsync("local");

        Assert.NotNull(result);
        Assert.Equal("app", Assert.Single(result.Containers).Stack);
        Assert.DoesNotContain(
            result.Stacks,
            item => item.Kind == DiagramStackKind.ReadOnly);
    }

    [Fact]
    public async Task GetDiagramAsync_creates_readonly_stacks_for_unknown_compose_projects()
    {
        var socket = CreateSocket();
        var containers = new[]
        {
            CreateContainer("c1", "db", "external"),
            CreateContainer("c2", "cache", "external"),
        };

        var service = CreateService(socket, [], containers);

        var result = await service.GetDiagramAsync("local");

        Assert.NotNull(result);
        var diagramStack = Assert.Single(result.Stacks);
        Assert.Null(diagramStack.Id);
        Assert.Equal("external", diagramStack.ProjectName);
        Assert.Equal(DiagramStackKind.ReadOnly, diagramStack.Kind);
        Assert.All(result.Containers, container => Assert.Equal("external", container.Stack));
    }

    [Fact]
    public async Task GetDiagramAsync_leaves_unlabeled_containers_unbound()
    {
        var socket = CreateSocket();
        var containers = new[]
        {
            CreateContainer("c1", "manual", stack: null),
            CreateContainer("c2", "blank", "  "),
        };

        var service = CreateService(socket, [], containers);

        var result = await service.GetDiagramAsync("local");

        Assert.NotNull(result);
        Assert.Empty(result.Stacks);
        Assert.Equal(2, result.Containers.Count);
        Assert.All(result.Containers, container => Assert.Null(container.Stack));
    }

    [Fact]
    public async Task GetDiagramAsync_includes_managed_stacks_with_no_containers()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "empty");

        var service = CreateService(socket, [stack], []);

        var result = await service.GetDiagramAsync("local");

        Assert.NotNull(result);
        var diagramStack = Assert.Single(result.Stacks);
        Assert.Equal("empty", diagramStack.ProjectName);
        Assert.Equal(DiagramStackKind.Managed, diagramStack.Kind);
        Assert.Empty(result.Containers);
    }

    [Fact]
    public async Task GetDiagramAsync_lists_managed_stacks_before_readonly_stacks()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "zeta");
        var containers = new[]
        {
            CreateContainer("c1", "web", "zeta"),
            CreateContainer("c2", "db", "alpha"),
        };

        var service = CreateService(socket, [stack], containers);

        var result = await service.GetDiagramAsync("local");

        Assert.NotNull(result);
        Assert.Equal(2, result.Stacks.Count);
        Assert.Equal(DiagramStackKind.Managed, result.Stacks[0].Kind);
        Assert.Equal("zeta", result.Stacks[0].ProjectName);
        Assert.Equal(DiagramStackKind.ReadOnly, result.Stacks[1].Kind);
        Assert.Equal("alpha", result.Stacks[1].ProjectName);
    }

    private static DeviceDiagramService CreateService(
        SocketEntity? socket,
        IReadOnlyList<StackEntity> stacks,
        IReadOnlyCollection<ContainerDto> containers)
    {
        var socketRepository = A.Fake<ISocketRepository>();
        var stackRepository = A.Fake<IStackRepository>();
        var deviceQueryService = A.Fake<IDeviceQueryService>();

        A.CallTo(() => socketRepository.GetByNameAsync("local", A<CancellationToken>._))
            .Returns(socket);
        A.CallTo(() => socketRepository.GetByNameAsync("missing", A<CancellationToken>._))
            .Returns((SocketEntity?)null);

        if (socket is not null)
        {
            A.CallTo(() => stackRepository.ListBySocketIdAsync(socket.Id, A<CancellationToken>._))
                .Returns(stacks.ToList());
            A.CallTo(() => deviceQueryService.GetContainersAsync("local", A<CancellationToken>._))
                .Returns(containers);
        }

        return new DeviceDiagramService(socketRepository, stackRepository, deviceQueryService);
    }

    private static SocketEntity CreateSocket()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = "local",
            Address = "http://127.0.0.1:5101",
            CreatedAt = DateTimeOffset.UtcNow,
        };

    private static StackEntity CreateStack(Guid socketId, string projectName)
        => new()
        {
            Id = Guid.NewGuid(),
            SocketId = socketId,
            ProjectName = projectName,
            ComposeYaml = "services: {}",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow,
        };

    private static ContainerDto CreateContainer(string id, string name, string? stack)
        => new()
        {
            Id = id,
            Name = name,
            State = "running",
            Stack = stack,
            TotalBytes = 0,
        };
}
