using System.Net;
using System.Text;
using FakeItEasy;
using Dome.Business.Devices;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Compose;
using Dome.Socket.Contracts.Containers;
using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using SocketEntity = Dome.Domain.Sockets.Socket;
using StackEntity = Dome.Domain.Stacks.Stack;

namespace Dome.Business.Tests.Devices;

public sealed class DeviceStackDeployServiceTests
{
    [Fact]
    public async Task DeployAsync_returns_device_not_found_when_socket_is_missing()
    {
        var service = CreateService(socket: null, stack: CreateStack(Guid.NewGuid()), A.Fake<IDomeSocketApi>());

        var result = await service.DeployAsync("local", Guid.NewGuid(), TestContext.Current.CancellationToken);

        Assert.True(result.IsDeviceNotFound);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeployAsync_returns_stack_not_found_when_stack_is_missing()
    {
        var socket = CreateSocket();
        var service = CreateService(socket, stack: null, A.Fake<IDomeSocketApi>());

        var result = await service.DeployAsync("local", Guid.NewGuid(), TestContext.Current.CancellationToken);

        Assert.True(result.IsStackNotFound);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeployAsync_returns_stack_not_found_when_stack_belongs_to_another_device()
    {
        var socket = CreateSocket();
        var stack = CreateStack(Guid.NewGuid());
        var service = CreateService(socket, stack, A.Fake<IDomeSocketApi>());

        var result = await service.DeployAsync("local", stack.Id, TestContext.Current.CancellationToken);

        Assert.True(result.IsStackNotFound);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeployAsync_returns_success_when_socket_compose_up_succeeds()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id);
        var socketApi = A.Fake<IDomeSocketApi>();
        var service = CreateService(socket, stack, socketApi);

        var result = await service.DeployAsync("local", stack.Id, TestContext.Current.CancellationToken);

        Assert.True(result.IsSuccess);
        A.CallTo(() => socketApi.UpComposeAsync(
                A<ComposeUpRequest>.That.Matches(request =>
                    request.ProjectName == stack.ProjectName
                    && request.ComposeYaml == stack.ComposeYaml),
                A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeployAsync_returns_failed_when_socket_returns_502()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id);
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.UpComposeAsync(A<ComposeUpRequest>._, A<CancellationToken>._))
            .Throws(await CreateApiExceptionAsync(
                HttpStatusCode.BadGateway,
                """{"message":"image not found"}"""));

        var service = CreateService(socket, stack, socketApi);

        var result = await service.DeployAsync("local", stack.Id, TestContext.Current.CancellationToken);

        Assert.True(result.IsFailed);
        Assert.Equal("image not found", result.Error);
    }

    [Fact]
    public async Task DeployAsync_returns_failed_when_socket_returns_400()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id);
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.UpComposeAsync(A<ComposeUpRequest>._, A<CancellationToken>._))
            .Throws(await CreateApiExceptionAsync(
                HttpStatusCode.BadRequest,
                """{"message":"Compose file is required."}"""));

        var service = CreateService(socket, stack, socketApi);

        var result = await service.DeployAsync("local", stack.Id, TestContext.Current.CancellationToken);

        Assert.True(result.IsFailed);
        Assert.Equal("Compose file is required.", result.Error);
    }

    [Fact]
    public async Task DeleteAsync_returns_device_not_found_when_socket_is_missing()
    {
        var service = CreateService(null, CreateStack(Guid.NewGuid()), A.Fake<IDomeSocketApi>());

        var result = await service.DeleteAsync("local", Guid.NewGuid(), false, TestContext.Current.CancellationToken);

        Assert.True(result.IsDeviceNotFound);
    }

    [Fact]
    public async Task DeleteAsync_returns_stack_not_found_when_stack_is_missing()
    {
        var service = CreateService(CreateSocket(), null, A.Fake<IDomeSocketApi>());

        var result = await service.DeleteAsync("local", Guid.NewGuid(), false, TestContext.Current.CancellationToken);

        Assert.True(result.IsStackNotFound);
    }

    [Fact]
    public async Task DeleteAsync_returns_stack_not_found_when_stack_belongs_to_another_device()
    {
        var stack = CreateStack(Guid.NewGuid());
        var socketApi = A.Fake<IDomeSocketApi>();
        var service = CreateService(CreateSocket(), stack, socketApi);

        var result = await service.DeleteAsync("local", stack.Id, false, TestContext.Current.CancellationToken);

        Assert.True(result.IsStackNotFound);
        A.CallTo(() => socketApi.DownComposeAsync(A<ComposeDownRequest>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task DeleteAsync_forwards_request_and_deletes_row_on_success()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id);
        var socketApi = A.Fake<IDomeSocketApi>();
        var stackRepository = A.Fake<IStackRepository>();
        var service = CreateService(socket, stack, socketApi, stackRepository);

        var result = await service.DeleteAsync("local", stack.Id, true, TestContext.Current.CancellationToken);

        Assert.True(result.IsSuccess);
        A.CallTo(() => socketApi.DownComposeAsync(
                A<ComposeDownRequest>.That.Matches(r => r.ProjectName == stack.ProjectName && r.RemoveVolumes),
                A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => stackRepository.DeleteAsync(stack, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteAsync_keeps_row_when_socket_returns_502()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id);
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.DownComposeAsync(A<ComposeDownRequest>._, A<CancellationToken>._))
            .Throws(await CreateApiExceptionAsync(HttpStatusCode.BadGateway, """{"message":"boom"}"""));
        var stackRepository = A.Fake<IStackRepository>();
        var service = CreateService(socket, stack, socketApi, stackRepository);

        var result = await service.DeleteAsync("local", stack.Id, false, TestContext.Current.CancellationToken);

        Assert.True(result.IsFailed);
        Assert.Equal("boom", result.Error);
        A.CallTo(() => stackRepository.DeleteAsync(A<StackEntity>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task DeleteUnmanagedAsync_returns_device_not_found_when_socket_is_missing()
    {
        var service = CreateService(null, null, A.Fake<IDomeSocketApi>());

        var result = await service.DeleteUnmanagedAsync("local", "proj", false, TestContext.Current.CancellationToken);

        Assert.True(result.IsDeviceNotFound);
    }

    [Fact]
    public async Task DeleteUnmanagedAsync_rejects_managed_project_name()
    {
        var socket = CreateSocket();
        var stack = CreateStack(socket.Id);
        var socketApi = A.Fake<IDomeSocketApi>();
        var service = CreateService(socket, stack, socketApi, managed: [stack]);

        var result = await service.DeleteUnmanagedAsync("local", "MY-STACK", false, TestContext.Current.CancellationToken);

        Assert.True(result.IsStackNotFound);
        A.CallTo(() => socketApi.DeleteContainersAsync(A<DeleteContainersRequest>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task DeleteUnmanagedAsync_returns_stack_not_found_when_no_containers_match()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.GetContainersAsync(A<CancellationToken>._))
            .Returns(new GetContainersResponse { Containers = [Container("a", "other")] });
        var service = CreateService(CreateSocket(), null, socketApi);

        var result = await service.DeleteUnmanagedAsync("local", "proj", false, TestContext.Current.CancellationToken);

        Assert.True(result.IsStackNotFound);
        A.CallTo(() => socketApi.DeleteContainersAsync(A<DeleteContainersRequest>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task DeleteUnmanagedAsync_deletes_only_matching_containers()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.GetContainersAsync(A<CancellationToken>._))
            .Returns(new GetContainersResponse
            {
                Containers =
                [
                    Container("a", " Proj "),
                    Container("b", "other"),
                    Container("c", null)
                ]
            });
        A.CallTo(() => socketApi.DeleteContainersAsync(A<DeleteContainersRequest>._, A<CancellationToken>._))
            .Returns(new DeleteContainersResponse
            {
                Results = [new DeleteContainerResult { ContainerId = "a", Status = DeleteContainerStatus.NotFound }]
            });
        var service = CreateService(CreateSocket(), null, socketApi);

        var result = await service.DeleteUnmanagedAsync("local", "proj", true, TestContext.Current.CancellationToken);

        Assert.True(result.IsSuccess);
        A.CallTo(() => socketApi.DeleteContainersAsync(
                A<DeleteContainersRequest>.That.Matches(r =>
                    r.RemoveVolumes && r.ContainerIds.Count == 1 && r.ContainerIds[0] == "a"),
                A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task DeleteUnmanagedAsync_returns_failed_with_container_error()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.GetContainersAsync(A<CancellationToken>._))
            .Returns(new GetContainersResponse { Containers = [Container("a", "proj")] });
        A.CallTo(() => socketApi.DeleteContainersAsync(A<DeleteContainersRequest>._, A<CancellationToken>._))
            .Returns(new DeleteContainersResponse
            {
                Results = [new DeleteContainerResult { ContainerId = "a", Status = DeleteContainerStatus.Failed, Error = "in use" }]
            });
        var service = CreateService(CreateSocket(), null, socketApi);

        var result = await service.DeleteUnmanagedAsync("local", "proj", false, TestContext.Current.CancellationToken);

        Assert.True(result.IsFailed);
        Assert.Equal("in use", result.Error);
    }

    private static DeviceStackDeployService CreateService(
        SocketEntity? socket,
        StackEntity? stack,
        IDomeSocketApi socketApi,
        IStackRepository? stackRepository = null,
        IReadOnlyList<StackEntity>? managed = null)
    {
        var socketRepository = A.Fake<ISocketRepository>();
        stackRepository ??= A.Fake<IStackRepository>();
        if (socket is not null)
        {
            A.CallTo(() => stackRepository.ListBySocketIdAsync(socket.Id, A<CancellationToken>._))
                .Returns(managed ?? []);
        }
        var socketApiFactory = A.Fake<IDomeSocketApiFactory>();

        A.CallTo(() => socketRepository.GetByNameAsync("local", A<CancellationToken>._))
            .Returns(socket);
        if (stack is not null)
        {
            A.CallTo(() => stackRepository.GetByIdAsync(stack.Id, A<CancellationToken>._))
                .Returns(stack);
        }

        A.CallTo(() => socketApiFactory.Create(A<string>._))
            .Returns(socketApi);

        return new DeviceStackDeployService(
            socketRepository,
            stackRepository,
            socketApiFactory,
            NullLogger<DeviceStackDeployService>.Instance);
    }

    private static ContainerResponse Container(string id, string? stack)
        => new() { Id = id, Name = id, State = "running", Stack = stack };

    private static SocketEntity CreateSocket()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = "local",
            Address = "http://127.0.0.1:5101",
            CreatedAt = DateTimeOffset.UtcNow,
        };

    private static StackEntity CreateStack(Guid socketId)
        => new()
        {
            Id = Guid.NewGuid(),
            SocketId = socketId,
            ProjectName = "my-stack",
            ComposeYaml = "services: {}",
            CreatedAt = DateTimeOffset.UtcNow,
            UpdatedAt = DateTimeOffset.UtcNow
        };

    private static async Task<ApiException> CreateApiExceptionAsync(
        HttpStatusCode statusCode,
        string content)
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "http://127.0.0.1/api/compose/up");
        using var response = new HttpResponseMessage(statusCode)
        {
            RequestMessage = request,
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

        return await ApiException.Create(request, HttpMethod.Post, response, new RefitSettings());
    }
}
