namespace HomelabDocs.Web.Models;

public sealed record DiagramEdge(
    string Id,
    string SourceId,
    string TargetId,
    string? Label = null);
