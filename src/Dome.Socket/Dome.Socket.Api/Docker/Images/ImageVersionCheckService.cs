using Dome.Socket.Api.Docker.Clients;
using Dome.Socket.Api.Docker.Registries;
using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Api.Docker.Images;

internal sealed class ImageVersionCheckService : IImageVersionCheckService
{
    private const int MaxDegreeOfParallelism = 4;

    private readonly IDockerEngineClient _dockerEngineClient;
    private readonly IReadOnlyList<IRegistryProvider> _registryProviders;

    public ImageVersionCheckService(
        IDockerEngineClient dockerEngineClient,
        IEnumerable<IRegistryProvider> registryProviders)
    {
        _dockerEngineClient = dockerEngineClient;
        _registryProviders = registryProviders.ToArray();
    }

    public async Task<CheckImageVersionsResponse> CheckAsync(
        CheckImageVersionsRequest request,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(request);

        var images = (request.Images ?? Array.Empty<ImageVersionCheckRequestItem>()).ToArray();
        var results = new ImageVersionCheckResponse[images.Length];
        await Parallel.ForEachAsync(
            images.Select(static (item, index) => (item, index)),
            new ParallelOptions
            {
                MaxDegreeOfParallelism = MaxDegreeOfParallelism,
                CancellationToken = cancellationToken,
            },
            async (pair, token) =>
            {
                results[pair.index] = await CheckOneAsync(pair.item, token);
            });

        return new CheckImageVersionsResponse
        {
            Results = results,
        };
    }

    private async Task<ImageVersionCheckResponse> CheckOneAsync(
        ImageVersionCheckRequestItem item,
        CancellationToken cancellationToken)
    {
        var image = item.Image?.Trim() ?? string.Empty;
        var parsed = ImageReferenceParser.TryParse(image);
        string? localDigest = null;
        if (!string.IsNullOrWhiteSpace(item.ImageId))
        {
            var repoDigests = await _dockerEngineClient.GetImageRepoDigestsAsync(
                item.ImageId,
                cancellationToken);
            localDigest = ImageDigestMapper.Resolve(image, repoDigests);
        }

        if (parsed is null)
        {
            return new ImageVersionCheckResponse
            {
                Image = image,
                ImageId = item.ImageId,
                LocalDigest = localDigest,
                Unsupported = true,
            };
        }

        var provider = _registryProviders.FirstOrDefault(candidate => candidate.CanHandle(parsed));
        if (provider is null)
        {
            return new ImageVersionCheckResponse
            {
                Image = image,
                ImageId = item.ImageId,
                LocalDigest = localDigest,
                Unsupported = true,
            };
        }

        try
        {
            var info = await provider.GetVersionInfoAsync(parsed, cancellationToken);
            return new ImageVersionCheckResponse
            {
                Image = image,
                ImageId = item.ImageId,
                LocalDigest = localDigest ?? parsed.Digest,
                RemoteDigest = info.RemoteDigest,
                Tags = info.Tags,
            };
        }
        catch (Exception ex) when (ex is not OperationCanceledException)
        {
            return new ImageVersionCheckResponse
            {
                Image = image,
                ImageId = item.ImageId,
                LocalDigest = localDigest ?? parsed.Digest,
                Error = ex.Message,
            };
        }
    }
}
