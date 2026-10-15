using Dome.Business.Devices;
using Dome.Business.Sockets;
using Dome.Domain.Images;
using Dome.Domain.Sockets;
using Dome.Shared.Containers;
using Dome.Shared.Images;
using Dome.Socket.Contracts.Images;
using Microsoft.Extensions.Logging;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Images;

public sealed class DeviceImageVersionChecker : IDeviceImageVersionChecker
{
    internal static readonly TimeSpan CacheTtl = TimeSpan.FromHours(24);

    private readonly ISocketRepository _socketRepository;
    private readonly IDeviceQueryService _deviceQueryService;
    private readonly IDomeSocketApiFactory _socketApiFactory;
    private readonly IImageVersionCacheRepository _cacheRepository;
    private readonly ILogger<DeviceImageVersionChecker> _logger;

    public DeviceImageVersionChecker(
        ISocketRepository socketRepository,
        IDeviceQueryService deviceQueryService,
        IDomeSocketApiFactory socketApiFactory,
        IImageVersionCacheRepository cacheRepository,
        ILogger<DeviceImageVersionChecker> logger)
    {
        _socketRepository = socketRepository;
        _deviceQueryService = deviceQueryService;
        _socketApiFactory = socketApiFactory;
        _cacheRepository = cacheRepository;
        _logger = logger;
    }

    public async Task<IReadOnlyCollection<ImageVersionCheckDto>?> GetAsync(
        string deviceName,
        CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(deviceName);

        var socket = await _socketRepository.GetByNameAsync(deviceName, cancellationToken);
        if (socket is null)
        {
            return null;
        }

        var containers = await _deviceQueryService.GetContainersAsync(deviceName, cancellationToken);
        if (containers is null)
        {
            return null;
        }

        var now = DateTimeOffset.UtcNow;
        var canonicalByContainer = containers
            .Select(container => (
                Container: container,
                Canonical: CanonicalReference(container.Image)))
            .ToArray();
        var cacheKeys = canonicalByContainer
            .Select(static item => item.Canonical)
            .Where(static key => key.Length > 0)
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToArray();
        var cached = (await _cacheRepository.GetByReferencesAsync(cacheKeys, cancellationToken))
            .ToDictionary(entry => entry.ImageReference, StringComparer.OrdinalIgnoreCase);

        var expired = canonicalByContainer
            .Where(item => item.Canonical.Length > 0 && IsExpired(cached, item.Canonical, now))
            .GroupBy(static item => item.Canonical, StringComparer.OrdinalIgnoreCase)
            .Select(static group => group.First().Container)
            .ToArray();

        if (expired.Length > 0)
        {
            await RefreshExpiredAsync(socket, expired, cached, now, cancellationToken);
        }

        return canonicalByContainer
            .Select(item => ClassifyContainer(item.Container, item.Canonical, cached))
            .ToArray();
    }

    private async Task RefreshExpiredAsync(
        SocketEntity socket,
        IReadOnlyCollection<ContainerDto> expired,
        Dictionary<string, ImageVersionCache> cached,
        DateTimeOffset now,
        CancellationToken cancellationToken)
    {
        try
        {
            var api = _socketApiFactory.Create(socket.Address);
            var response = await api.CheckImageVersionsAsync(
                new CheckImageVersionsRequest
                {
                    Images = expired
                        .Select(static container => new ImageVersionCheckRequestItem
                        {
                            Image = container.Image,
                            ImageId = string.IsNullOrWhiteSpace(container.ImageId)
                                ? null
                                : container.ImageId,
                        })
                        .ToArray(),
                },
                cancellationToken);

            foreach (var result in response.Results)
            {
                var canonical = CanonicalReference(result.Image);
                if (canonical.Length == 0)
                {
                    continue;
                }

                var entry = new ImageVersionCache
                {
                    ImageReference = canonical,
                    RemoteDigest = result.RemoteDigest,
                    RemoteTags = string.Join('\n', result.Tags),
                    Unsupported = result.Unsupported,
                    LastError = result.Error,
                    CheckedAt = now,
                };
                await _cacheRepository.UpsertAsync(entry, cancellationToken);
                cached[canonical] = entry;
            }
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            _logger.LogWarning(
                ex,
                "Failed to refresh image version cache from socket '{SocketName}'.",
                socket.Name);
        }
    }

    private static ImageVersionCheckDto ClassifyContainer(
        ContainerDto container,
        string canonical,
        IReadOnlyDictionary<string, ImageVersionCache> cached)
    {
        cached.TryGetValue(canonical, out var entry);
        var snapshot = entry is null
            ? new ImageVersionSnapshot()
            : new ImageVersionSnapshot
            {
                RemoteDigest = entry.RemoteDigest,
                Tags = SplitTags(entry.RemoteTags),
                Unsupported = entry.Unsupported,
                Error = entry.LastError,
            };
        var localDigest = string.IsNullOrWhiteSpace(container.Digest)
            ? null
            : container.Digest;
        var classification = ImageVersionClassifier.Classify(
            container.Image,
            localDigest,
            snapshot);

        return new ImageVersionCheckDto
        {
            ContainerId = container.Id,
            Image = container.Image,
            CurrentTag = classification.CurrentTag,
            NewestTag = classification.NewestTag,
            Kind = classification.Kind,
            CheckedAt = entry?.CheckedAt,
        };
    }

    private static bool IsExpired(
        IReadOnlyDictionary<string, ImageVersionCache> cached,
        string canonical,
        DateTimeOffset now)
    {
        if (!cached.TryGetValue(canonical, out var entry))
        {
            return true;
        }

        return now - entry.CheckedAt >= CacheTtl;
    }

    private static string CanonicalReference(string? image)
        => ImageReferenceParser.TryParse(image)?.Canonical ?? image?.Trim() ?? string.Empty;

    private static IReadOnlyCollection<string> SplitTags(string remoteTags)
    {
        if (string.IsNullOrWhiteSpace(remoteTags))
        {
            return Array.Empty<string>();
        }

        return remoteTags.Split(
            '\n',
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
    }
}
