using System.Net;
using FakeItEasy;
using Dome.Business.Sockets;
using Dome.Domain.Sockets;
using Dome.Shared.Sockets;
using Dome.Socket.Contracts;
using Dome.Socket.Contracts.Health;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Logging.Abstractions;
using Refit;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Tests.Sockets;

public sealed class SocketServiceTests
{
    private readonly ISocketRepository _socketRepository = A.Fake<ISocketRepository>();
    private readonly IDomeSocketApiFactory _socketApiFactory = A.Fake<IDomeSocketApiFactory>();
    private readonly SocketApiKeys _apiKeys = new(new EphemeralDataProtectionProvider());

    [Fact]
    public async Task CreateAsync_returns_key_and_stores_only_protected_form()
    {
        SocketEntity? stored = null;
        A.CallTo(() => _socketRepository.AddAsync(A<SocketEntity>._, A<CancellationToken>._))
            .Invokes((SocketEntity socket, CancellationToken _) => stored = socket);
        var service = CreateService("0.3.2");

        var result = await service.CreateAsync(
            "local",
            "https://127.0.0.1:5110",
            TestContext.Current.CancellationToken);

        Assert.True(result.IsSuccess);
        Assert.False(string.IsNullOrWhiteSpace(result.ApiKey));
        Assert.NotNull(stored);
        Assert.NotEqual(result.ApiKey, stored.ProtectedApiKey);
        Assert.Equal(result.ApiKey, _apiKeys.Unprotect(stored.ProtectedApiKey));
        Assert.Null(stored.CertificateFingerprint);
    }

    [Fact]
    public void SocketResponse_has_no_api_key_property()
    {
        Assert.DoesNotContain(
            typeof(SocketResponse).GetProperties(),
            property => property.Name.Contains("Key", StringComparison.OrdinalIgnoreCase));
    }

    [Fact]
    public async Task RotateKeyAsync_returns_new_key_and_clears_pin()
    {
        var socket = CreateSocket();
        socket.ProtectedApiKey = _apiKeys.Protect("old-key");
        A.CallTo(() => _socketRepository.GetByIdAsync(socket.Id, A<CancellationToken>._))
            .Returns(socket);
        var service = CreateService("0.3.2");

        var result = await service.RotateKeyAsync(socket.Id, TestContext.Current.CancellationToken);

        Assert.True(result.IsSuccess);
        Assert.NotEqual("old-key", result.ApiKey);
        Assert.Equal(result.ApiKey, _apiKeys.Unprotect(socket.ProtectedApiKey));
        Assert.Null(socket.CertificateFingerprint);
        A.CallTo(() => _socketRepository.UpdateAsync(socket, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task RotateKeyAsync_returns_not_found_for_unknown_socket()
    {
        A.CallTo(() => _socketRepository.GetByIdAsync(A<Guid>._, A<CancellationToken>._))
            .Returns((SocketEntity?)null);
        var service = CreateService("0.3.2");

        var result = await service.RotateKeyAsync(Guid.NewGuid(), TestContext.Current.CancellationToken);

        Assert.True(result.IsNotFound);
    }

    [Fact]
    public async Task UpdateAsync_clears_pin_when_address_changes()
    {
        var socket = CreateSocket();
        A.CallTo(() => _socketRepository.GetByIdAsync(socket.Id, A<CancellationToken>._))
            .Returns(socket);
        var service = CreateService("0.3.2");

        await service.UpdateAsync(
            socket.Id,
            socket.Name,
            "https://10.0.0.2:5110",
            TestContext.Current.CancellationToken);

        Assert.Null(socket.CertificateFingerprint);
    }

    [Fact]
    public async Task UpdateAsync_keeps_pin_when_address_is_unchanged()
    {
        var socket = CreateSocket();
        A.CallTo(() => _socketRepository.GetByIdAsync(socket.Id, A<CancellationToken>._))
            .Returns(socket);
        var service = CreateService("0.3.2");

        await service.UpdateAsync(
            socket.Id,
            "renamed",
            socket.Address,
            TestContext.Current.CancellationToken);

        Assert.Equal("ab", socket.CertificateFingerprint);
    }

    [Fact]
    public async Task GetStatusesAsync_needs_pairing_when_unpinned_and_pairing_fails()
    {
        var socket = CreateSocket();
        socket.CertificateFingerprint = null;
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => _socketApiFactory.PairAsync(socket, A<CancellationToken>._))
            .Returns((string?)null);
        var service = CreateService(socket, socketApi, "0.3.2");

        var status = Assert.Single(
            await service.GetStatusesAsync(TestContext.Current.CancellationToken));

        Assert.Equal(SocketState.NeedsPairing, status.State);
        Assert.False(status.IsReachable);
        A.CallTo(() => socketApi.GetHealthAsync(A<CancellationToken>._)).MustNotHaveHappened();
        A.CallTo(() => _socketRepository.UpdateAsync(A<SocketEntity>._, A<CancellationToken>._))
            .MustNotHaveHappened();
    }

    [Fact]
    public async Task GetStatusesAsync_persists_pin_when_pairing_succeeds()
    {
        var socket = CreateSocket();
        socket.CertificateFingerprint = null;
        var socketApi = A.Fake<IDomeSocketApi>();
        A.CallTo(() => _socketApiFactory.PairAsync(socket, A<CancellationToken>._))
            .Returns("cd");
        A.CallTo(() => socketApi.GetHealthAsync(A<CancellationToken>._))
            .Returns(new SocketHealthResponse { DockerReachable = true, Version = "0.3.2" });
        var service = CreateService(socket, socketApi, "0.3.2");

        var status = Assert.Single(
            await service.GetStatusesAsync(TestContext.Current.CancellationToken));

        Assert.Equal(SocketState.Online, status.State);
        Assert.Equal("cd", socket.CertificateFingerprint);
        A.CallTo(() => _socketRepository.UpdateAsync(socket, A<CancellationToken>._))
            .MustHaveHappenedOnceExactly();
    }

    [Fact]
    public async Task GetStatusesAsync_is_unauthorized_when_socket_rejects_key()
    {
        var socketApi = A.Fake<IDomeSocketApi>();
        var unauthorized = await ApiException.Create(
            new HttpRequestMessage(HttpMethod.Get, "https://127.0.0.1:5110/api/health"),
            HttpMethod.Get,
            new HttpResponseMessage(HttpStatusCode.Unauthorized),
            new RefitSettings());
        A.CallTo(() => socketApi.GetHealthAsync(A<CancellationToken>._))
            .Throws(unauthorized);
        var service = CreateService(CreateSocket(), socketApi, "0.3.2");

        var status = Assert.Single(
            await service.GetStatusesAsync(TestContext.Current.CancellationToken));

        Assert.Equal(SocketState.Unauthorized, status.State);
        Assert.False(status.IsReachable);
    }

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
        Assert.Equal(SocketState.Offline, status.State);
        Assert.Null(status.Version);
        Assert.False(status.NeedsUpdate);
    }

    private SocketService CreateService(
        SocketEntity socket,
        IDomeSocketApi socketApi,
        string serverVersion)
    {
        A.CallTo(() => _socketRepository.ListAsync(A<CancellationToken>._))
            .Returns([socket]);
        A.CallTo(() => _socketApiFactory.Create(socket))
            .Returns(socketApi);

        return CreateService(serverVersion);
    }

    private SocketService CreateService(string serverVersion)
    {
        var applicationVersion = A.Fake<IApplicationVersion>();
        A.CallTo(() => applicationVersion.Current)
            .Returns(serverVersion);

        return new SocketService(
            _socketRepository,
            _socketApiFactory,
            applicationVersion,
            _apiKeys,
            NullLogger<SocketService>.Instance);
    }

    private static SocketEntity CreateSocket()
        => new()
        {
            Id = Guid.NewGuid(),
            Name = "local",
            Address = "https://127.0.0.1:5110",
            CertificateFingerprint = "ab",
            CreatedAt = DateTimeOffset.UtcNow,
        };
}
