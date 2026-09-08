using FakeItEasy;
using Dome.Business.Stacks;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using SocketEntity = Dome.Domain.Sockets.Socket;
using StackEntity = Dome.Domain.Stacks.Stack;

namespace Dome.Business.Tests.Stacks;

public sealed class StackServiceTests
{
    [Fact]
    public async Task CreateAsync_sets_default_yaml_and_project_name()
    {
        var socket = CreateSocket();
        var stackRepository = A.Fake<IStackRepository>();
        StackEntity? added = null;
        A.CallTo(() => stackRepository.AddAsync(A<StackEntity>._, A<CancellationToken>._))
            .Invokes((StackEntity stack, CancellationToken _) => added = stack);

        var service = CreateService(socket, stackRepository);

        var result = await service.CreateAsync("local", "my-stack");

        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Stack);
        Assert.Equal("my-stack", result.Stack.ProjectName);
        Assert.Equal(StackService.DefaultComposeYaml, result.Stack.ComposeYaml);
        Assert.NotNull(added);
        Assert.Equal(socket.Id, added.SocketId);
        Assert.Equal("services: {}", added.ComposeYaml);
    }

    [Fact]
    public async Task CreateAsync_returns_not_found_when_device_is_missing()
    {
        var service = CreateService(socket: null, A.Fake<IStackRepository>());

        var result = await service.CreateAsync("missing", "app");

        Assert.True(result.IsNotFound);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task CreateAsync_returns_conflict_when_project_name_exists()
    {
        var socket = CreateSocket();
        var stackRepository = A.Fake<IStackRepository>();
        A.CallTo(() => stackRepository.ProjectNameExistsAsync(
                socket.Id,
                "app",
                null,
                A<CancellationToken>._))
            .Returns(true);

        var service = CreateService(socket, stackRepository);

        var result = await service.CreateAsync("local", "app");

        Assert.True(result.IsConflict);
        Assert.False(result.IsSuccess);
        A.CallTo(() => stackRepository.AddAsync(A<StackEntity>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task CreateAsync_returns_invalid_when_project_name_is_blank()
    {
        var service = CreateService(CreateSocket(), A.Fake<IStackRepository>());

        var result = await service.CreateAsync("local", "   ");

        Assert.False(result.IsSuccess);
        Assert.Equal("Project name is required.", result.Error);
    }

    [Theory]
    [InlineData("My Stack")]
    [InlineData("!!!")]
    [InlineData("-app")]
    [InlineData("_app")]
    [InlineData("APP")]
    public async Task CreateAsync_returns_invalid_when_project_name_is_not_a_compose_project(
        string projectName)
    {
        var stackRepository = A.Fake<IStackRepository>();
        var service = CreateService(CreateSocket(), stackRepository);

        var result = await service.CreateAsync("local", projectName);

        Assert.False(result.IsSuccess);
        Assert.Contains("valid Compose project name", result.Error);
        A.CallTo(() => stackRepository.AddAsync(A<StackEntity>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task UpdateAsync_updates_yaml_and_project_name()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "old");
        var stackRepository = A.Fake<IStackRepository>();
        A.CallTo(() => stackRepository.GetByIdAsync(stack.Id, A<CancellationToken>._))
            .Returns(stack);

        var service = CreateService(socket, stackRepository);
        const string yaml = "services:\n  web:\n    image: nginx\n";

        var result = await service.UpdateAsync(
            "local",
            stack.Id,
            "new-name",
            yaml);

        Assert.True(result.IsSuccess);
        Assert.Equal("new-name", result.Stack!.ProjectName);
        Assert.Equal(yaml, result.Stack.ComposeYaml);
        A.CallTo(() => stackRepository.UpdateAsync(stack, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task UpdateAsync_returns_not_found_when_stack_belongs_to_another_device()
    {
        var socket = CreateSocket();
        var stack = CreateStack(Guid.NewGuid(), "app");
        var stackRepository = A.Fake<IStackRepository>();
        A.CallTo(() => stackRepository.GetByIdAsync(stack.Id, A<CancellationToken>._))
            .Returns(stack);

        var service = CreateService(socket, stackRepository);

        var result = await service.UpdateAsync("local", stack.Id, "app", "services: {}");

        Assert.True(result.IsNotFound);
    }

    [Fact]
    public async Task UpdateAsync_returns_invalid_when_compose_yaml_is_blank()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "app");
        var stackRepository = A.Fake<IStackRepository>();
        A.CallTo(() => stackRepository.GetByIdAsync(stack.Id, A<CancellationToken>._))
            .Returns(stack);

        var service = CreateService(socket, stackRepository);

        var result = await service.UpdateAsync("local", stack.Id, "app", "  ");

        Assert.False(result.IsSuccess);
        Assert.Equal("Compose file is required.", result.Error);
    }

    [Fact]
    public async Task UpdateAsync_returns_invalid_when_compose_yaml_exceeds_512_kib()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "app");
        var stackRepository = A.Fake<IStackRepository>();
        A.CallTo(() => stackRepository.GetByIdAsync(stack.Id, A<CancellationToken>._))
            .Returns(stack);

        var service = CreateService(socket, stackRepository);
        var yaml = new string('a', StackService.MaxComposeYamlBytes + 1);

        var result = await service.UpdateAsync("local", stack.Id, "app", yaml);

        Assert.False(result.IsSuccess);
        Assert.Equal("Compose file must be 512 KiB or smaller.", result.Error);
        A.CallTo(() => stackRepository.UpdateAsync(A<StackEntity>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task ListAsync_returns_null_when_device_is_missing()
    {
        var service = CreateService(socket: null, A.Fake<IStackRepository>());

        var result = await service.ListAsync("missing");

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_returns_stack_for_the_device()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "app");
        var stackRepository = A.Fake<IStackRepository>();
        A.CallTo(() => stackRepository.GetByIdAsync(stack.Id, A<CancellationToken>._))
            .Returns(stack);

        var service = CreateService(socket, stackRepository);

        var result = await service.GetAsync("local", stack.Id);

        Assert.NotNull(result);
        Assert.Equal(stack.Id, result.Id);
        Assert.Equal("app", result.ProjectName);
    }

    [Fact]
    public async Task DeleteAsync_returns_null_when_device_is_missing()
    {
        var service = CreateService(socket: null, A.Fake<IStackRepository>());

        var result = await service.DeleteAsync("missing", Guid.NewGuid());

        Assert.Null(result);
    }

    [Fact]
    public async Task DeleteAsync_returns_false_when_stack_is_missing()
    {
        var socket = CreateSocket();
        var stackRepository = A.Fake<IStackRepository>();
        A.CallTo(() => stackRepository.GetByIdAsync(A<Guid>._, A<CancellationToken>._))
            .Returns((StackEntity?)null);

        var service = CreateService(socket, stackRepository);

        var result = await service.DeleteAsync("local", Guid.NewGuid());

        Assert.False(result);
        A.CallTo(() => stackRepository.DeleteAsync(A<StackEntity>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_removes_the_stack()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id, "app");
        var stackRepository = A.Fake<IStackRepository>();
        A.CallTo(() => stackRepository.GetByIdAsync(stack.Id, A<CancellationToken>._))
            .Returns(stack);

        var service = CreateService(socket, stackRepository);

        var result = await service.DeleteAsync("local", stack.Id);

        Assert.True(result);
        A.CallTo(() => stackRepository.DeleteAsync(stack, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    private static StackService CreateService(
        SocketEntity? socket,
        IStackRepository stackRepository)
    {
        var socketRepository = A.Fake<ISocketRepository>();
        A.CallTo(() => socketRepository.GetByNameAsync("local", A<CancellationToken>._))
            .Returns(socket);
        A.CallTo(() => socketRepository.GetByNameAsync("missing", A<CancellationToken>._))
            .Returns((SocketEntity?)null);

        return new StackService(socketRepository, stackRepository);
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
}
