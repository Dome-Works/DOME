using Dome.Socket.Api.Docker;
using Dome.Socket.Api.Docker.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Dome.Socket.Tests.Docker.Configuration;

public sealed class DockerEndpointOptionsTests
{
    [Fact]
    public void AddDomeSocket_without_endpoint_configuration_uses_local_socket()
    {
        var services = new ServiceCollection();
        services.AddDomeSocket(new ConfigurationBuilder().Build());
        using var provider = services.BuildServiceProvider();

        var options = provider.GetRequiredService<IOptions<DockerEndpointOptions>>();

        Assert.Equal("unix:///var/run/docker.sock", options.Value.Endpoint);
    }
}
