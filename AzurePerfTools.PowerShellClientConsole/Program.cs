using System;

namespace AzurePerfTools.PowerShellClientConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                RemotePowerShellClient client = new RemotePowerShellClient("PowerShellClient");

                string startResponse = client.StartPowerShell();
                Console.Write(startResponse);

                string command;
                do
                {
                    command = Console.ReadLine();
                    ExecuteCommand(client, command);
                } while (!string.Equals("exit", command, StringComparison.InvariantCultureIgnoreCase));

                client.Close();
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.ToString());
            }

            Console.WriteLine("\n\nPress any key to exit");
            Console.ReadKey();
        }

        private static void ExecuteCommand(RemotePowerShellClient client, string cmd)
        {
            string commandResponse = client.SendCommand(cmd);
            Console.Write(commandResponse);
        }

        private static void ExecuteCommandBatch(RemotePowerShellClient client, string[] commands)
        {
            foreach (string command in commands)
            {
                ExecuteCommand(client, command);
            }
        }
    }
}
