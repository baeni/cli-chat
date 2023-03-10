using System.Net.Sockets;

namespace CliChat.Lib.Interfaces
{
    public interface IClientApp : IApplication
    {
        TcpClient TcpClient { get; }

        string Username { get; }

        void Connect();

        void Disconnect();
    }
}
