using CliChat.Lib.Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

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
                var stream = tcpClient.GetStream();
                var bytes = new byte[256];
                int i = stream.Read(bytes, 0, bytes.Length);
                var message = Encoding.UTF8.GetString(bytes, 0, i);
                Console.WriteLine(message);
            }
        }

        public void Stop()
        {
            TcpListener.Stop();
        }
    }
}
