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

                var client = new ClientApp(Address, Port, message[..^14], tcpClient);
                Clients.Add(client);

                Task.Run(() => HandleIncomingTraffic(client));
            }
        }

        public void Stop()
        {
            foreach (var client in Clients)
            {
                client.TcpClient.Close();
                Clients.Remove(client);
            }

            TcpListener.Stop();
        }

        private void HandleIncomingTraffic(IClientApp client)
        {
            while (true)
            {
                var stream = client.TcpClient.GetStream();
                var buffer = new byte[256];
                int i = stream.Read(buffer, 0, buffer.Length);
                var message = Encoding.UTF8.GetString(buffer, 0, i);
                Console.WriteLine(message);

                foreach (var loopedClient in Clients.Where(x => x != client))
                {
                    if (!loopedClient.TcpClient.Connected)
                    {
                        loopedClient.TcpClient.Close();
                        Clients.Remove(loopedClient);
                        continue;
                    }

                    var forwardableMessage = $"{client.Username}: {Encoding.UTF8.GetString(buffer, 0, i)}";
                    var forwardableBuffer = Encoding.UTF8.GetBytes(forwardableMessage);
                    loopedClient.TcpClient.GetStream().Write(forwardableBuffer, 0, forwardableBuffer.Length);
                }
            }
        }
    }
}
