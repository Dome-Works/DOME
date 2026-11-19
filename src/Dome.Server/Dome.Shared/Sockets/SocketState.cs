using System.Text.Json.Serialization;

namespace Dome.Shared.Sockets;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SocketState
{
    Online,
    Offline,
    NeedsPairing,
    Unauthorized
}
