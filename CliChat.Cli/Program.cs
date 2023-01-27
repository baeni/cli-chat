using CliChat.Lib;
using CliChat.Lib.Enums;

namespace CliChat.Cli
{
    internal class Program
    {

        static void Main(string[] args)
        {
            // Checks if there are at least 3 arguments
            // and returns if there are less.
            ValidateConfiguration(args);

            var applicationType = TryParseApplicationType(args[0]);

            var address = args[1];
            var port = int.Parse(args[2]);

            switch (applicationType)
            {
                case ApplicationType.Server:
                    SetUpAsServer(address, port);
                    break;

                case ApplicationType.Client:
                    SetUpAsClient(address, port);
                    break;
            }
        }

        private static void ValidateConfiguration(string[] args)
        {
            if (args.Length < 3)
            {
                // TODO: Explain further
                Console.WriteLine("Application is missconfigured.");
                Console.ReadKey();
            }
        }

        private static ApplicationType? TryParseApplicationType(string argument)
        {
            ApplicationType? applicationType = null;

            try
            {
                applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), argument);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ApplicationType not valid.");
            }

            return applicationType;
        }

        private static void SetUpAsServer(string address, int port)
        {
            Console.WriteLine("Starting Server...");
            var server = new ServerApp(address, port);
            server.Start();
        }

        private static void SetUpAsClient(string address, int port)
        {
            Console.Write("What's your name?: ");
            var input = Console.ReadLine();
            var username = string.IsNullOrEmpty(input)
                ? "Unknown"
                : input;

            var client = new ClientApp(address, port, username);
            client.Connect();
        }
    }
}