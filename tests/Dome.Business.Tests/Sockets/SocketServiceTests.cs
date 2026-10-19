using FakeItEasy;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Health;
using Microsoft.Extensions.Logging.Abstractions;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Tests.Sockets;

public sealed class SocketServiceTests
{
    [Fact]
    public async Task GetStatusesAsync_does_not_need_update_when_versions_match()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.GetHealthAsync(A<CancellationToken>._))
            .Returns(new SocketHealthResponse
            {
                DockerReachable = true,
                Version = "0.3.2"
            });
        var service = CreateService(CreateSocket(), socketApi, "0.3.2");

        var status = Assert.Single(
            await service.GetStatusesAsync(TestContext.Current.CancellationToken));

        Assert.True(status.IsReachable);
        Assert.Equal("0.3.2", status.Version);
        Assert.False(status.NeedsUpdate);
    }

    [Fact]
    public async Task GetStatusesAsync_needs_update_when_versions_differ()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.GetHealthAsync(A<CancellationToken>._))
            .Returns(new SocketHealthResponse
            {
                DockerReachable = true,
                Version = "v0.3.1"
            });
        var service = CreateService(CreateSocket(), socketApi, "0.3.2");

        var status = Assert.Single(
            await service.GetStatusesAsync(TestContext.Current.CancellationToken));

        Assert.True(status.IsReachable);
        Assert.Equal("v0.3.1", status.Version);
        Assert.True(status.NeedsUpdate);
    }

    [Fact]
    public async Task GetStatusesAsync_treats_v_prefix_as_the_same_version()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.GetHealthAsync(A<CancellationToken>._))
            .Returns(new SocketHealthResponse
            {
                DockerReachable = true,
                Version = "v0.3.2"
            });
        var service = CreateService(CreateSocket(), socketApi, "0.3.2");

        var status = Assert.Single(
            await service.GetStatusesAsync(TestContext.Current.CancellationToken));

        Assert.False(status.NeedsUpdate);
    }

    [Fact]
    public async Task GetStatusesAsync_needs_update_when_reachable_socket_omits_version()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.GetHealthAsync(A<CancellationToken>._))
            .Returns(new SocketHealthResponse
            {
                DockerReachable = true
            });
        var service = CreateService(CreateSocket(), socketApi, "0.3.2");

        var status = Assert.Single(
            await service.GetStatusesAsync(TestContext.Current.CancellationToken));

        Assert.True(status.IsReachable);
        Assert.Null(status.Version);
        Assert.True(status.NeedsUpdate);
    }

    [Fact]
    public async Task GetStatusesAsync_does_not_need_update_when_socket_is_unreachable()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.GetHealthAsync(A<CancellationToken>._))
            .Throws(new HttpRequestException("connection refused"));
        var service = CreateService(CreateSocket(), socketApi, "0.3.2");

        var status = Assert.Single(
            await service.GetStatusesAsync(TestContext.Current.CancellationToken));

        Assert.False(status.IsReachable);
        Assert.Null(status.Version);
        Assert.False(status.NeedsUpdate);
    }

    private static SocketService CreateService(
        SocketEntity socket,
        IDomeSocketApi socketApi,
        string serverVersion)
    {
        var socketRepository = A.Fake<ISocketRepository>();
        var socketApiFactory = A.Fake<IDomeSocketApiFactory>();
        var applicationVersion = A.Fake<IApplicationVersion>();

        A.CallTo(() => socketRepository.ListAsync(A<CancellationToken>._))
            .Returns([socket]);
        A.CallTo(() => socketApiFactory.Create(socket.Address))
            .Returns(socketApi);
        A.CallTo(() => applicationVersion.Current)
            .Returns(serverVersion);

        return new SocketService(
            socketRepository,
            socketApiFactory,
            applicationVersion,
            NullLogger<SocketService>.Instance);
    }

    private static SocketEntity CreateSocket()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = "local",
            Address = "http://127.0.0.1:5110",
            CreatedAt = DateTimeOffset.UtcNow,
        };
}
