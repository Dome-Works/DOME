using Dome.Socket.Api.Docker.Images;
using Dome.Socket.Api.Docker.Registries;
using Dome.Socket.Contracts.Images;
using Dome.Socket.Tests.Docker.Registries;

namespace Dome.Socket.Tests.Docker.Images;

public sealed class ImageVersionCheckServiceTests
{
    [Fact]
    public async Task CheckAsync_marks_unsupported_hosts_without_calling_a_provider()
    {
        var httpClient = new HttpClient(new ScriptedHttpMessageHandler());
        var service = new ImageVersionCheckService(
            new StubDockerEngineClient(),
            [
                new DockerHubRegistryProvider(httpClient),
                new GhcrRegistryProvider(httpClient),
            ]);

        var result = await service.CheckAsync(
            new CheckImageVersionsRequest
            {
                Images =
                [
                    new ImageVersionCheckRequestItem
                    {
                        Image = "quay.io/prometheus/prometheus:v2.50.0",
                        ImageId = "sha256:local",
                    },
                ],
            },
            TestContext.Current.CancellationToken);

        var check = Assert.Single(result.Results);
        Assert.True(check.Unsupported);
        Assert.Null(check.Error);
        Assert.Equal("sha256:from-inspect", check.LocalDigest);
    }
}
