namespace Dome.Socket.Api.Docker.Compose;

public interface IDockerComposeClient
{
    Task<DockerComposeResult> UpAsync(
        string projectName,
        string composeYaml,
        CancellationToken cancellationToken = default);
}
