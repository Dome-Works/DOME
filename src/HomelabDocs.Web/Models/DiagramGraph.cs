namespace HomelabDocs.Web.Models;

public sealed record DiagramGraph(
    IReadOnlyCollection<DiagramNode> Nodes,
    IReadOnlyCollection<DiagramEdge> Edges);
