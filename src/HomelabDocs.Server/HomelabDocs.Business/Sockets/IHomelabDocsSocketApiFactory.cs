using HomelabDocs.Socket.Contracts;

namespace HomelabDocs.Business.Sockets;

public interface IHomelabDocsSocketApiFactory
{
    IHomelabDocsSocketApi Create(string address);
}
