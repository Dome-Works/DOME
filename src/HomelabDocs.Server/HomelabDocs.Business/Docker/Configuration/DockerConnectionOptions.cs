namespace HomelabDocs.Business.Docker.Configuration;

public sealed class DockerConnectionOptions
{
    public const string SectionName = "Docker";

    public const string DefaultDockerSocket =
        "unix:///var/run/docker.sock";

    public string Endpoint { get; init; } = DefaultDockerSocket;
}
