namespace HomelabDocs.Socket.Tests.Docker.Clients;

internal sealed record FallbackNameSample(
    IList<string>? Names,
    string? ContainerId);
