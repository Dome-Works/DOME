namespace HomelabDocs.Business.Docker.Configuration;

public sealed class DockerConnectionOptions
{
    public const string SectionName = "Docker";

    public IList<DockerConnectionEntry> Connections { get; init; } =
        new List<DockerConnectionEntry>();
}
