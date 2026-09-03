using Dome.Socket.Contracts;

namespace Dome.Business.Sockets;

public interface IDomeSocketApiFactory
{
    IDomeSocketApi Create(string address);
}
