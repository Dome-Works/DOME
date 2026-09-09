using System.Net;
using System.Text;
using FakeItEasy;
using Dome.Business.Devices;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Domain.Stacks;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Compose;
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

        var result = await service.DeployAsync("local", Guid.NewGuid());

        Assert.True(result.IsDeviceNotFound);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeployAsync_returns_stack_not_found_when_stack_is_missing()
    {
        var socket = CreateSocket();
        var service = CreateService(socket, stack: null, A.Fake<IDomeSocketApi>());

        var result = await service.DeployAsync("local", Guid.NewGuid());

        Assert.True(result.IsStackNotFound);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task DeployAsync_returns_stack_not_found_when_stack_belongs_to_another_device()
    {
        var socket = CreateSocket();
        var stack = CreateStack(Guid.NewGuid());
        var service = CreateService(socket, stack, A.Fake<IDomeSocketApi>());

        var result = await service.DeployAsync("local", stack.Id);

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

        var result = await service.DeployAsync("local", stack.Id);

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

        var result = await service.DeployAsync("local", stack.Id);

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

        var result = await service.DeployAsync("local", stack.Id);

        Assert.True(result.IsFailed);
        Assert.Equal("Compose file is required.", result.Error);
    }

    private static DeviceStackDeployService CreateService(
        SocketEntity? socket,
        StackEntity? stack,
        IDomeSocketApi socketApi)
    {
        var socketRepository = A.Fake<ISocketRepository>();
        var stackRepository = A.Fake<IStackRepository>();
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
