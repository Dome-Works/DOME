using FakeItEasy;
using Dome.Business.Devices;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Socket.Contracts;
using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Tests.Devices;

public sealed class DeviceContainerCommandServiceTests
{
    [Fact]
    public async Task StartAsync_returns_device_not_found_when_socket_is_missing()
    {
        var service = CreateService(socket: null, socketApi: A.Fake<IDomeSocketApi>());

        var result = await service.StartAsync("local", "container-1");

        Assert.True(result.IsDeviceNotFound);
        Assert.False(result.IsSuccess);
    }

    [Fact]
    public async Task StartAsync_returns_success_when_socket_starts_the_container()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        var service = CreateService(CreateSocket(), socketApi);

        var result = await service.StartAsync("local", "container-1");

        Assert.True(result.IsSuccess);
        A.CallTo(() => socketApi.StartContainerAsync("container-1", A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task StopAsync_returns_success_when_socket_stops_the_container()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        var service = CreateService(CreateSocket(), socketApi);

        var result = await service.StopAsync("local", "container-1");

        Assert.True(result.IsSuccess);
        A.CallTo(() => socketApi.StopContainerAsync("container-1", A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task StartAsync_returns_container_not_found_when_socket_returns_404()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.StartContainerAsync("missing", A<CancellationToken>._))
            .Throws(await CreateNotFoundApiExceptionAsync());

        var service = CreateService(CreateSocket(), socketApi);

        var result = await service.StartAsync("local", "missing");

        Assert.True(result.IsContainerNotFound);
        Assert.False(result.IsSuccess);
    }

    private static DeviceContainerCommandService CreateService(
        SocketEntity? socket,
        IDomeSocketApi socketApi)
    {
        var socketRepository = A.Fake<ISocketRepository>();
        var socketApiFactory = A.Fake<IDomeSocketApiFactory>();

        A.CallTo(() => socketRepository.GetByNameAsync("local", A<CancellationToken>._))
            .Returns(socket);
        A.CallTo(() => socketApiFactory.Create(A<string>._))
            .Returns(socketApi);

        return new DeviceContainerCommandService(
            socketRepository,
            socketApiFactory,
            NullLogger<DeviceContainerCommandService>.Instance);
    }

    private static SocketEntity CreateSocket()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = "local",
            Address = "http://127.0.0.1:5101",
            CreatedAt = DateTimeOffset.UtcNow,
        };

    private static async Task<ApiException> CreateNotFoundApiExceptionAsync()
    {
        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "http://127.0.0.1/api/containers/missing/start");
        using var response = new HttpResponseMessage(System.Net.HttpStatusCode.NotFound)
        {
            RequestMessage = request,
        };

        return await ApiException.Create(request, HttpMethod.Post, response, new RefitSettings());
    }
}
