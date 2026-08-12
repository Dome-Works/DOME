namespace HomelabDocs.Business.Docker.Configuration;

public sealed class DockerConnectionEntry
{
    public string Name { get; init; } = string.Empty;

    public string Endpoint { get; init; } = string.Empty;
}
