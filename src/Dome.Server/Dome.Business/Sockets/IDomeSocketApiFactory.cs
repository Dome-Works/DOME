using Dome.Socket.Contracts;
using SocketEntity = Dome.Domain.Sockets.Socket;

namespace Dome.Business.Sockets;

public interface IDomeSocketApiFactory
{
    IDomeSocketApi Create(SocketEntity socket);

    Task<string?> PairAsync(SocketEntity socket, CancellationToken cancellationToken);
}
