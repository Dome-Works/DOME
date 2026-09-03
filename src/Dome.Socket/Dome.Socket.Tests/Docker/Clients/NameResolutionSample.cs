namespace HomelabDocs.Socket.Tests.Docker.Clients;

internal sealed record NameResolutionSample(
    string UsableName,
    int PrefixCount,
    IList<string> Suffix);
