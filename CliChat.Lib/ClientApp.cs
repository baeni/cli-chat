using CliChat.Lib.Interfaces;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CliChat.Lib
{
    public class ClientApp : IClientApp
    {
        public ClientApp(string address, int port, string username, TcpClient? tcpClient = null)
        {
            TcpClient = tcpClient ?? new TcpClient();
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
            try
            {
                TcpClient.Connect(IPAddress.Parse(Address), Port);

            Console.WriteLine("Connection established!");

                var message = $"{Username} has appeared.";
                var bytes = Encoding.UTF8.GetBytes(message);
                var stream = TcpClient.GetStream();
                stream.Write(bytes, 0, bytes.Length);
            }
            catch (SocketException)
            {
                Console.WriteLine("Server is not available.");
            }

            Console.ReadKey();
        }

        public void Disconnect()
        {

        private void HandleIncomingTraffic()
        {
            while (true)
            {
                var stream = TcpClient.GetStream();
                var buffer = new byte[256];
                int i = stream.Read(buffer, 0, buffer.Length);
                var message = Encoding.UTF8.GetString(buffer, 0, i);
                Console.WriteLine(message);
            }
        }
    }
}
