using FakeItEasy;
using HomelabDocs.Business.Devices;
using HomelabDocs.Business.Sockets;
using HomelabDocs.Domain.Sockets;
using HomelabDocs.Socket.Contracts;
using Microsoft.Extensions.Logging.Abstractions;
using GetContainersResponse = HomelabDocs.Socket.Contracts.Containers.GetContainersResponse;
using ContainerVolumeResponse = HomelabDocs.Socket.Contracts.Containers.ContainerVolumeResponse;
using SocketContainerResponse = HomelabDocs.Socket.Contracts.Containers.ContainerResponse;
using SocketEntity = HomelabDocs.Domain.Sockets.Socket;

namespace HomelabDocs.Business.Tests.Devices;

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
                Stack = "homelabdocs",
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
        var socketApiFactory = A.Fake<IHomelabDocsSocketApiFactory>();
        var socketApi = A.Fake<IHomelabDocsSocketApi>();

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
