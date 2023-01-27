using CliChat.Lib.Interfaces;
using System.Net;
using System.Net.Sockets;

namespace CliChat.Lib
{
    public class ClientApp : IClientApp
    {
        public ClientApp(string address, int port, string username)
        {
            TcpClient = new TcpClient();
            Address = address;
            Port = port;
            Username = username;
        }

        public TcpClient TcpClient { get; }

        public string Address { get; }

        public int Port { get; }

        public string Username { get; }


        public void Connect()
        {
            TcpClient.Connect(IPAddress.Parse(Address), Port);
        }

        public void Disconnect()
        {

        }
    }
}
