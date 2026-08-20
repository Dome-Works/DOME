using System.Text.Json;
using HomelabDocs.Socket.Contracts;
using Refit;

namespace HomelabDocs.Business.Sockets;

public sealed class HomelabDocsSocketApiFactory : IHomelabDocsSocketApiFactory
{
    private static readonly RefitSettings RefitSettings = new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(
            new JsonSerializerOptions(JsonSerializerDefaults.Web))
    };

    private readonly IHttpClientFactory _httpClientFactory;

    public HomelabDocsSocketApiFactory(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IHomelabDocsSocketApi Create(string address)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(address);

        var client = _httpClientFactory.CreateClient(nameof(HomelabDocsSocketApiFactory));
        client.BaseAddress = new Uri(address, UriKind.Absolute);
        return RestService.For<IHomelabDocsSocketApi>(client, RefitSettings);
    }
}
