using System.Text.Json.Serialization;

namespace Dome.Socket.Api.Docker.Registries;

internal sealed class OciTokenResponse
{
    [JsonPropertyName("token")]
    public string? Token { get; init; }

    [JsonPropertyName("access_token")]
    public string? AccessToken { get; init; }
}
