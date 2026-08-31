namespace HomelabDocs.Socket.Tests.Docker.Clients;

internal sealed record StackSample(
    string Project,
    int LeadingSpaces,
    int TrailingSpaces,
    Dictionary<string, string> ExtraLabels);
