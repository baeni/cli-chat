using CliChat.Lib.Interfaces;
using System.Net;
using System.Net.Sockets;

namespace CliChat.Lib
{
    public class ServerApp : IServerApp
    {
        public ServerApp(string address, int port)
        {
            TcpListener = new TcpListener(IPAddress.Parse(address), port);
            Address = address;
            Port = port;
        }

        public TcpListener TcpListener { get; }

        public string Address { get; }

        public int Port { get; }

        public List<IClientApp> Clients { get; } = new List<IClientApp>();

        public void Start()
        {
            TcpListener.Start();

            while (true)
            {
                var tcpClient = TcpListener.AcceptTcpClient();
                Console.WriteLine("A client has connected.");
            }
        }

        public void Stop()
        {
            TcpListener.Stop();
        }
    }
}
