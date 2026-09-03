using System.Net;
using Docker.DotNet;

namespace HomelabDocs.Socket.Api.Docker.Clients;

internal static class DockerContainerLifecycleExceptionMapper
{
    public static bool TryMap(
        DockerApiException exception,
        out DockerContainerLifecycleResult result)
    {
        ArgumentNullException.ThrowIfNull(exception);

        if (exception is DockerContainerNotFoundException
            || exception.StatusCode == HttpStatusCode.NotFound)
        {
            result = DockerContainerLifecycleResult.NotFound;
            return true;
        }

        if (exception.StatusCode == HttpStatusCode.NotModified)
        {
            result = DockerContainerLifecycleResult.Succeeded;
            return true;
        }

        result = default;
        return false;
    }
}
