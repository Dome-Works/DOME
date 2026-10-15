using FakeItEasy;
using Dome.Business.Devices;
using Dome.Business.Images;
using Dome.Business.Sockets;
using Dome.Domain.Images;
using Dome.Domain.Sockets;
using Dome.Shared.Containers;
using Dome.Shared.Images;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Images;
using Microsoft.Extensions.Logging.Abstractions;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Tests.Images;

public sealed class DeviceImageVersionCheckerTests
{
    [Fact]
    public async Task GetAsync_returns_null_when_device_is_missing()
    {
        var service = CreateService(socket: null, [], []);

        var result = await service.GetAsync("missing", TestContext.Current.CancellationToken);

        Assert.Null(result);
    }

    [Fact]
    public async Task GetAsync_uses_cached_tags_when_fresh()
    {
        var socket = CreateSocket();
        var containers = new[]
        {
            CreateContainer("c1", "nginx:1.27.3"),
        };
        var cache = new[]
        {
            new ImageVersionCache
            {
                Id = Guid.NewGuid(),
                ImageReference = "docker.io/library/nginx:1.27.3",
                RemoteDigest = "sha256:local",
                RemoteTags = "1.27.3\n1.28.0",
                CheckedAt = DateTimeOffset.UtcNow.AddHours(-1),
            },
        };
        var socketApi = A.Fake<IDomeSocketApi>();
        var service = CreateService(socket, containers, cache, socketApi);

        var result = await service.GetAsync("local", TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        var check = Assert.Single(result);
        Assert.Equal(ImageVersionKind.Minor, check.Kind);
        Assert.Equal("1.28.0", check.NewestTag);
        A.CallTo(() => socketApi.CheckImageVersionsAsync(
                A<CheckImageVersionsRequest>._,
                A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task GetAsync_refreshes_expired_cache_from_socket()
    {
        var socket = CreateSocket();
        var containers = new[]
        {
            CreateContainer("c1", "nginx:1.27.3", "sha256:local"),
        };
        var cacheRepository = A.Fake<IImageVersionCacheRepository>();
        A.CallTo(() => cacheRepository.GetByReferencesAsync(
                A<IReadOnlyCollection<string>>._,
                A<CancellationToken>._))
            .Returns([]);
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => socketApi.CheckImageVersionsAsync(
                A<CheckImageVersionsRequest>._,
                A<CancellationToken>._))
            .Returns(new CheckImageVersionsResponse
            {
                Results =
                [
                    new ImageVersionCheckResponse
                    {
                        Image = "nginx:1.27.3",
                        RemoteDigest = "sha256:local",
                        Tags = ["1.27.3", "2.0.0"],
                    },
                ],
            });
        var service = CreateService(
            socket,
            containers,
            cacheRepository,
            socketApi);

        var result = await service.GetAsync("local", TestContext.Current.CancellationToken);

        Assert.NotNull(result);
        var check = Assert.Single(result);
        Assert.Equal(ImageVersionKind.Major, check.Kind);
        Assert.Equal("2.0.0", check.NewestTag);
        A.CallTo(() => cacheRepository.UpsertAsync(
                A<ImageVersionCache>.That.Matches(entry =>
                    entry.ImageReference == "docker.io/library/nginx:1.27.3"),
                A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    private static DeviceImageVersionChecker CreateService(
        SocketEntity? socket,
        IReadOnlyCollection<ContainerDto> containers,
        IReadOnlyList<ImageVersionCache> cache,
        IDomeSocketApi? socketApi = null)
    {
        var cacheRepository = A.Fake<IImageVersionCacheRepository>();
        A.CallTo(() => cacheRepository.GetByReferencesAsync(
                A<IReadOnlyCollection<string>>._,
                A<CancellationToken>._))
            .Returns(cache.ToList());
        return CreateService(
            socket,
            containers,
            cacheRepository,
            socketApi ?? A.Fake<IDomeSocketApi>());
    }

    private static DeviceImageVersionChecker CreateService(
        SocketEntity? socket,
        IReadOnlyCollection<ContainerDto> containers,
        IImageVersionCacheRepository cacheRepository,
        IDomeSocketApi socketApi)
    {
        var socketRepository = A.Fake<ISocketRepository>();
        var deviceQueryService = A.Fake<IDeviceQueryService>();
        var socketApiFactory = A.Fake<IDomeSocketApiFactory>();

        A.CallTo(() => socketRepository.GetByNameAsync("local", A<CancellationToken>._))
            .Returns(socket);
        A.CallTo(() => socketRepository.GetByNameAsync("missing", A<CancellationToken>._))
            .Returns((SocketEntity?)null);
        if (socket is not null)
        {
            A.CallTo(() => deviceQueryService.GetContainersAsync("local", A<CancellationToken>._))
                .Returns(containers.ToList());
            A.CallTo(() => socketApiFactory.Create(socket.Address))
                .Returns(socketApi);
        }

        return new DeviceImageVersionChecker(
            socketRepository,
            deviceQueryService,
            socketApiFactory,
            cacheRepository,
            NullLogger<DeviceImageVersionChecker>.Instance);
    }

    private static SocketEntity CreateSocket()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = "local",
            Address = "http://127.0.0.1:5101",
            CreatedAt = DateTimeOffset.UtcNow,
        };

    private static ContainerDto CreateContainer(string id, string image, string? digest = "sha256:local")
        => new()
        {
            Id = id,
            Name = id,
            State = "running",
            Image = image,
            ImageId = "sha256:image",
            Digest = digest,
        };
}
