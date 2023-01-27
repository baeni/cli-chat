using CliChat.Lib;
using CliChat.Lib.Enums;

namespace CliChat.Cli
{
    internal class Program
    {

        static void Main(string[] args)
        {
            if (args.Length < 3)
            {
                // TODO: Explain further
                Console.WriteLine("Application is missconfigured.");
                Console.ReadKey();
                return;
            }

            ApplicationType applicationType;

            try
            {
                applicationType = (ApplicationType)Enum.Parse(typeof(ApplicationType), args[0]);
            }
            catch (ArgumentException)
            {
                Console.WriteLine("ApplicationType not valid.");
                return;
            }

            if (applicationType == ApplicationType.Server)
            {
                Console.WriteLine("Starting Server...");
                var server = new ServerApp(args[1], int.Parse(args[2]));
                server.Start();

                Console.ReadKey();
            }
            else if (applicationType == ApplicationType.Client)
            {
                Console.Write("What's your name?: ");
                var input = Console.ReadLine();
                var username = string.IsNullOrEmpty(input)
                    ? "Unknown"
                    : input;

                Console.WriteLine($"Welcome, {username}!");
                var client = new ClientApp(args[1], int.Parse(args[2]), username);
                client.Connect();

                Console.ReadKey();
            }
        }
    }
}