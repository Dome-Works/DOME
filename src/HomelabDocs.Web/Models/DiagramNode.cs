namespace HomelabDocs.Web.Models;

public sealed record DiagramNode(
    string Id,
    string Label,
    string Type,
    string? Status = null);
