using System.Net;
using Docker.DotNet;
using HomelabDocs.Socket.Api.Docker.Clients;

namespace HomelabDocs.Socket.Tests.Docker.Clients;

public sealed class DockerContainerLifecycleExceptionMapperTests
{
    [Fact]
    public void TryMap_maps_container_not_found_exception()
    {
        var exception = new DockerContainerNotFoundException(
            HttpStatusCode.NotFound,
            "no such container");

        var mapped = DockerContainerLifecycleExceptionMapper.TryMap(exception, out var result);

        Assert.True(mapped);
        Assert.Equal(DockerContainerLifecycleResult.NotFound, result);
    }

    [Fact]
    public void TryMap_maps_not_found_status_code()
    {
        var exception = new DockerApiException(HttpStatusCode.NotFound, "missing");

        var mapped = DockerContainerLifecycleExceptionMapper.TryMap(exception, out var result);

        Assert.True(mapped);
        Assert.Equal(DockerContainerLifecycleResult.NotFound, result);
    }

    [Fact]
    public void TryMap_maps_not_modified_as_success()
    {
        var exception = new DockerApiException(HttpStatusCode.NotModified, "already started");

        var mapped = DockerContainerLifecycleExceptionMapper.TryMap(exception, out var result);

        Assert.True(mapped);
        Assert.Equal(DockerContainerLifecycleResult.Succeeded, result);
    }

    [Fact]
    public void TryMap_does_not_map_other_status_codes()
    {
        var exception = new DockerApiException(HttpStatusCode.InternalServerError, "engine error");

        var mapped = DockerContainerLifecycleExceptionMapper.TryMap(exception, out var result);

        Assert.False(mapped);
        Assert.Equal(default, result);
    }
}
