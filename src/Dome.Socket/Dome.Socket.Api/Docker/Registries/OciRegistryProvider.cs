using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.RegularExpressions;
using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Api.Docker.Registries;

internal abstract class OciRegistryProvider : IRegistryProvider
{
    private const int MaxTagPages = 25;
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    private static readonly Regex NextLinkRegex = new(
        """<(?<url>[^>]+)>\s*;\s*rel="?next"?""",
        RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

    private readonly HttpClient _httpClient;

    protected OciRegistryProvider(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    protected abstract string RegistryApiHost { get; }

    protected abstract string DefaultTokenRealm { get; }

    protected abstract string DefaultTokenService { get; }

    public abstract bool CanHandle(ImageReference reference);

    public async Task<RegistryVersionInfo> GetVersionInfoAsync(
        ImageReference reference,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(reference);

        string? token = null;
        string? remoteDigest = null;
        if (!string.IsNullOrWhiteSpace(reference.Tag))
        {
            var manifest = await SendAuthenticatedAsync(
                HttpMethod.Get,
                BuildManifestUrl(reference.Repository, reference.Tag),
                reference.Repository,
                token,
                cancellationToken);
            token = manifest.Token;
            remoteDigest = ReadDigest(manifest.Response);
            manifest.Response.Dispose();
        }

        var tags = await ListTagsAsync(reference.Repository, token, cancellationToken);
        return new RegistryVersionInfo
        {
            RemoteDigest = remoteDigest,
            Tags = tags,
        };
    }

    private async Task<IReadOnlyCollection<string>> ListTagsAsync(
        string repository,
        string? token,
        CancellationToken cancellationToken)
    {
        var tags = new List<string>();
        var nextUrl = BuildTagsUrl(repository);

        for (var page = 0; page < MaxTagPages && nextUrl is not null; page++)
        {
            var result = await SendAuthenticatedAsync(
                HttpMethod.Get,
                nextUrl,
                repository,
                token,
                cancellationToken);
            token = result.Token;

            await using var stream = await result.Response.Content.ReadAsStreamAsync(cancellationToken);
            var payload = await JsonSerializer.DeserializeAsync<OciTagListResponse>(
                stream,
                JsonOptions,
                cancellationToken);
            if (payload?.Tags is not null)
            {
                tags.AddRange(
                    payload.Tags.Where(static tag => !string.IsNullOrWhiteSpace(tag)));
            }

            nextUrl = ReadNextLink(result.Response, nextUrl);
            result.Response.Dispose();
        }

        return tags
            .Distinct(StringComparer.Ordinal)
            .ToArray();
    }

    private async Task<OciAuthenticatedResponse> SendAuthenticatedAsync(
        HttpMethod method,
        Uri url,
        string repository,
        string? token,
        CancellationToken cancellationToken)
    {
        using var request = CreateRegistryRequest(method, url, token);
        var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            response.EnsureSuccessStatusCode();
            return new OciAuthenticatedResponse
            {
                Response = response,
                Token = token,
            };
        }

        var challenge = response.Headers.WwwAuthenticate.ToString();
        response.Dispose();
        var acquired = await RequestAnonymousTokenAsync(
            challenge,
            repository,
            cancellationToken);
        if (string.IsNullOrWhiteSpace(acquired))
        {
            throw new HttpRequestException(
                $"Anonymous token request failed for '{RegistryApiHost}/{repository}'.");
        }

        using var retry = CreateRegistryRequest(method, url, acquired);
        var retried = await _httpClient.SendAsync(
            retry,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);
        retried.EnsureSuccessStatusCode();
        return new OciAuthenticatedResponse
        {
            Response = retried,
            Token = acquired,
        };
    }

    private async Task<string?> RequestAnonymousTokenAsync(
        string challenge,
        string repository,
        CancellationToken cancellationToken)
    {
        if (!TryParseChallenge(challenge, out var realm, out var service, out var scope))
        {
            realm = DefaultTokenRealm;
            service = DefaultTokenService;
            scope = $"repository:{repository}:pull";
        }

        scope ??= $"repository:{repository}:pull";
        var uri = BuildTokenUri(realm, service, scope);
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);
        using var response = await _httpClient.SendAsync(
            request,
            HttpCompletionOption.ResponseHeadersRead,
            cancellationToken);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        var payload = await JsonSerializer.DeserializeAsync<OciTokenResponse>(
            stream,
            JsonOptions,
            cancellationToken);
        return string.IsNullOrWhiteSpace(payload?.Token)
            ? payload?.AccessToken
            : payload.Token;
    }

    private static HttpRequestMessage CreateRegistryRequest(
        HttpMethod method,
        Uri url,
        string? token)
    {
        var request = new HttpRequestMessage(method, url);
        ApplyAcceptHeaders(request);
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return request;
    }

    private Uri BuildManifestUrl(string repository, string tag)
        => new($"https://{RegistryApiHost}/v2/{EncodeRepository(repository)}/manifests/{Uri.EscapeDataString(tag)}");

    private Uri BuildTagsUrl(string repository)
        => new($"https://{RegistryApiHost}/v2/{EncodeRepository(repository)}/tags/list");

    private static string EncodeRepository(string repository)
        => string.Join('/', repository.Split('/').Select(Uri.EscapeDataString));

    private static void ApplyAcceptHeaders(HttpRequestMessage request)
    {
        request.Headers.Accept.Clear();
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.docker.distribution.manifest.list.v2+json"));
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.oci.image.index.v1+json"));
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.docker.distribution.manifest.v2+json"));
        request.Headers.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/vnd.oci.image.manifest.v1+json"));
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    private static string? ReadDigest(HttpResponseMessage response)
    {
        if (response.Headers.TryGetValues("Docker-Content-Digest", out var values))
        {
            var digest = values.FirstOrDefault(static value => !string.IsNullOrWhiteSpace(value));
            return digest?.Trim();
        }

        return null;
    }

    private static Uri? ReadNextLink(HttpResponseMessage response, Uri current)
    {
        if (!response.Headers.TryGetValues("Link", out var values))
        {
            return null;
        }

        return values
            .Select(static value => NextLinkRegex.Match(value))
            .Where(static match => match.Success)
            .Select(match => Uri.TryCreate(current, match.Groups["url"].Value, out var next)
                ? next
                : null)
            .FirstOrDefault(static next => next is not null);
    }

    private static bool TryParseChallenge(
        string header,
        out string realm,
        out string service,
        out string? scope)
    {
        realm = string.Empty;
        service = string.Empty;
        scope = null;
        if (string.IsNullOrWhiteSpace(header))
        {
            return false;
        }

        realm = ReadChallengeValue(header, "realm") ?? string.Empty;
        service = ReadChallengeValue(header, "service") ?? string.Empty;
        scope = ReadChallengeValue(header, "scope");
        return realm.Length > 0;
    }

    private static string? ReadChallengeValue(string header, string key)
    {
        var match = Regex.Match(
            header,
            $"{Regex.Escape(key)}=\"(?<value>[^\"]+)\"",
            RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
        return match.Success ? match.Groups["value"].Value : null;
    }

    private static Uri BuildTokenUri(string realm, string service, string scope)
    {
        var builder = new UriBuilder(realm);
        var query = $"service={Uri.EscapeDataString(service)}&scope={Uri.EscapeDataString(scope)}";
        var existing = builder.Query.TrimStart('?');
        builder.Query = existing.Length == 0 ? query : $"{existing}&{query}";

        return builder.Uri;
    }
}
