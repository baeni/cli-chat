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
            address = IPAddress.Any.ToString();

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
                Console.WriteLine("Now accepting traffic.");

                var tcpClient = TcpListener.AcceptTcpClient();
                var stream = tcpClient.GetStream();
                var buffer = new byte[256];
                int i = stream.Read(buffer, 0, buffer.Length);
                var message = Encoding.UTF8.GetString(buffer, 0, i);
                Console.WriteLine(message);
            }
        }

        public void Stop()
        {
            TcpListener.Stop();
        }
    }
}
