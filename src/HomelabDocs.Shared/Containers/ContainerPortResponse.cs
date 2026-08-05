namespace HomelabDocs.Shared.Containers;

public sealed record ContainerPortResponse
{
    public required string Type { get; init; }

    public required int PrivatePort { get; init; }

    public int? PublicPort { get; init; }

    public string? IpAddress { get; init; }
}
