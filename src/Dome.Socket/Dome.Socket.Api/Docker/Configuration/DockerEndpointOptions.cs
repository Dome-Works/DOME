namespace Dome.Socket.Api.Docker.Configuration;

public sealed class DockerEndpointOptions
{
    public const string SectionName = "Docker";

    public const string DefaultEndpoint = "unix:///var/run/docker.sock";

    public string Endpoint { get; set; } = DefaultEndpoint;
}
