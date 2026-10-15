using System.Text.Json.Serialization;

namespace Dome.Socket.Api.Docker.Registries;

internal sealed class OciTagListResponse
{
    [JsonPropertyName("tags")]
    public string[]? Tags { get; init; }
}
