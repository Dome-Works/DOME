using System.Text.Json.Serialization;

namespace Dome.Socket.Contracts.Containers;

// Attribute pins string serialization on both sides regardless of each side's JsonSerializerOptions.
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum DeleteContainerStatus
{
    Deleted,
    NotFound,
    Failed
}
