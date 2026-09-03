using FakeItEasy;
using Dome.Business.Devices;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Socket.Contracts;
using Microsoft.Extensions.Logging.Abstractions;
using GetContainersResponse = Dome.Socket.Contracts.Containers.GetContainersResponse;
using ContainerVolumeResponse = Dome.Socket.Contracts.Containers.ContainerVolumeResponse;
using SocketContainerResponse = Dome.Socket.Contracts.Containers.ContainerResponse;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Tests.Devices;

public sealed class DeviceQueryServiceTests
{
    [Fact]
    public async Task GetContainersAsync_calculates_total_bytes_from_volume_sizes()
    {
        var containers = new[]
        {
            new SocketContainerResponse
            {
                Id = "container-1",
                Name = "api",
                State = "running",
                Stack = "dome",
                Volumes =
                [
                    new ContainerVolumeResponse
                    {
                        Name = "data",
                        Destination = "/var/lib/app",
                        Type = "volume",
                        ReadOnly = false,
                        SizeBytes = 2048,
                    },
                    new ContainerVolumeResponse
                    {
                        Name = "config",
                        Destination = "/etc/app",
                        Type = "volume",
                        ReadOnly = true,
                        SizeBytes = 1024,
                    },
                ],
            },
        };

        var socket = new SocketEntity
        {
            Id = Guid.NewGuid(),
            Name = "local",
            Address = "unix:///var/run/docker.sock",
            CreatedAt = DateTimeOffset.UtcNow,
        };
        var socketRepository = A.Fake<ISocketRepository>();
        var socketApiFactory = A.Fake<IDomeSocketApiFactory>();
        var socketApi = A.Fake<IDomeSocketApi>();

        A.CallTo(() => socketRepository.GetByNameAsync("local", A<CancellationToken>._))
            .Returns(socket);
        A.CallTo(() => socketApiFactory.Create(socket.Address))
            .Returns(socketApi);
        A.CallTo(() => socketApi.GetContainersAsync(A<CancellationToken>._))
            .Returns(new GetContainersResponse { Containers = containers });

        var service = new DeviceQueryService(
            socketRepository,
            socketApiFactory,
            NullLogger<DeviceQueryService>.Instance);

        var result = await service.GetContainersAsync("local");

        Assert.NotNull(result);
        var container = Assert.Single(result);
        Assert.Equal(3072, container.TotalBytes);
    }
}
