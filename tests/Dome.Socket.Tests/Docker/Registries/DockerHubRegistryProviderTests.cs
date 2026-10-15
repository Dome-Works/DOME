using System.Net;
using System.Net.Http.Headers;
using Dome.Socket.Api.Docker.Registries;
using Dome.Socket.Contracts.Images;

namespace Dome.Socket.Tests.Docker.Registries;

public sealed class DockerHubRegistryProviderTests
{
    [Fact]
    public void CanHandle_official_and_docker_hub_images()
    {
        var provider = new DockerHubRegistryProvider(new HttpClient());

        Assert.True(provider.CanHandle(Parse("nginx:1.27.3")));
        Assert.True(provider.CanHandle(Parse("library/nginx:1.27.3")));
        Assert.False(provider.CanHandle(Parse("ghcr.io/dome-works/dome:1.0.0")));
        Assert.False(provider.CanHandle(Parse("quay.io/prometheus/prometheus:v2.50.0")));
    }

    [Fact]
    public async Task GetVersionInfoAsync_follows_anonymous_token_challenge()
    {
        var handler = new ScriptedHttpMessageHandler();
        handler.Enqueue(request =>
        {
            Assert.Equal(
                "https://registry-1.docker.io/v2/library/nginx/manifests/latest",
                request.RequestUri?.ToString());
            Assert.Null(request.Headers.Authorization);
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Headers.WwwAuthenticate.Add(
                new AuthenticationHeaderValue(
                    "Bearer",
                    "realm=\"https://auth.docker.io/token\",service=\"registry.docker.io\",scope=\"repository:library/nginx:pull\""));
            return response;
        });
        handler.Enqueue(request =>
        {
            Assert.Equal(
                "https://auth.docker.io/token?service=registry.docker.io&scope=repository%3Alibrary%2Fnginx%3Apull",
                request.RequestUri?.ToString());
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("""{"token":"anonymous-token"}"""),
            };
        });
        handler.Enqueue(request =>
        {
            Assert.Equal("Bearer", request.Headers.Authorization?.Scheme);
            Assert.Equal("anonymous-token", request.Headers.Authorization?.Parameter);
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            response.Headers.TryAddWithoutValidation("Docker-Content-Digest", "sha256:abc");
            response.Content = new StringContent("{}");
            return response;
        });
        handler.Enqueue(request =>
        {
            Assert.Contains("/v2/library/nginx/tags/list", request.RequestUri?.ToString());
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("""{"tags":["latest","1.27.3"]}"""),
            };
        });

        var provider = new DockerHubRegistryProvider(new HttpClient(handler));
        var info = await provider.GetVersionInfoAsync(
            Parse("nginx:latest"),
            TestContext.Current.CancellationToken);

        Assert.Equal("sha256:abc", info.RemoteDigest);
        Assert.Equal(["latest", "1.27.3"], info.Tags);
    }

    private static ImageReference Parse(string image)
    {
        var reference = ImageReferenceParser.TryParse(image);
        Assert.NotNull(reference);
        return reference;
    }
}
