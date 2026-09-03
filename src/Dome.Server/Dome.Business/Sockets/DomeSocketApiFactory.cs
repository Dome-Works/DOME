using System.Text.Json;
using Dome.Socket.Contracts;
using Refit;

namespace Dome.Business.Sockets;

public sealed class DomeSocketApiFactory : IDomeSocketApiFactory
{
    private static readonly RefitSettings RefitSettings = new()
    {
        ContentSerializer = new SystemTextJsonContentSerializer(
            new JsonSerializerOptions(JsonSerializerDefaults.Web))
    };

    private readonly IHttpClientFactory _httpClientFactory;

    public DomeSocketApiFactory(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public IDomeSocketApi Create(string address)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(address);

        var client = _httpClientFactory.CreateClient(nameof(DomeSocketApiFactory));
        client.BaseAddress = new Uri(address, UriKind.Absolute);
        return RestService.For<IDomeSocketApi>(client, RefitSettings);
    }
}
