namespace Dome.Socket.Api.Docker.Configuration;

public sealed class DockerComposeOptions
{
    public const string SectionName = "Docker:Compose";

    public string WorkingRoot { get; set; } = string.Empty;

    public string DockerCliPath { get; set; } = "docker";
}
