using HomelabDocs.Socket.Api.Docker.Configuration;

namespace HomelabDocs.Socket.Tests.Docker.Configuration;

public sealed class DockerEndpointValidatorTests
{
    [Fact]
    public void Validate_accepts_unix_socket()
    {
        var uri = DockerEndpointValidator.Validate("unix:///var/run/docker.sock");
        Assert.Equal("unix", uri.Scheme);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_rejects_missing_endpoint(string? endpoint)
    {
        Assert.Throws<InvalidOperationException>(() => DockerEndpointValidator.Validate(endpoint));
    }

    [Theory]
    [InlineData("tcp://127.0.0.1:2375")]
    [InlineData("http://127.0.0.1:2375")]
    [InlineData("npipe://./pipe/docker_engine")]
    public void Validate_rejects_non_unix_schemes(string endpoint)
    {
        Assert.Throws<InvalidOperationException>(() => DockerEndpointValidator.Validate(endpoint));
    }

    [Fact]
    public void Validate_rejects_relative_uri()
    {
        Assert.Throws<InvalidOperationException>(
            static () => DockerEndpointValidator.Validate("var/run/docker.sock"));
    }
}
