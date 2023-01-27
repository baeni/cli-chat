using System.Net.Sockets;

namespace CliChat.Lib.Interfaces
{
    public interface IServerApp : IApplication
    {
        TcpListener TcpListener { get; }

        List<IClientApp> Clients { get; }

        void Start();

        void Stop();
    }
}
