using Docker.DotNet;
using HomelabDocs.Shared.Devices;

namespace HomelabDocs.Business.Docker.Clients;

public interface IDockerClientRegistry
{
    IReadOnlyList<DeviceResponse> GetDevices();

    bool TryGetClient(string deviceName, out IDockerClient client);
}
