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

                Console.WriteLine("Press any key to start");
                Console.ReadKey();
                Console.WriteLine();

                string startResponse = client.StartPowerShell(Providers, PerfCounters);
                Console.Write(startResponse);

                //RestartIis(client, "test1");

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
            Console.WriteLine(commandResponse);
        }

        private static void ExecuteCommandBatch(RemotePowerShellClient client, string[] commands)
        {
            foreach (string command in commands)
            {
                ExecuteCommand(client, command);
            }
        }

        private static void StartPerfCounters(RemotePowerShellClient client)
        {
            ExecuteCommandBatch(client, new string[] {
                @"del perfcounters*.blg",
                @"logman create counter infrastructure_counters -cf perfCounters.txt -si 1 -o perfcounters.blg",
                @"logman start infrastructure_counters",
            });
        }

        private static void StopPerfCounters(RemotePowerShellClient client)
        {
            ExecuteCommandBatch(client, new string[] {
                @"logman stop infrastructure_counters",
                @"logman delete infrastructure_counters",
                @"copy .\*.blg $testOutputDir",
            });
        }

        private static void RestartIis(RemotePowerShellClient client, string testName)
        {
            ExecuteCommandBatch(client, new string[] {
                string.Format(@"$testOutputDir = $perfOutputDir + ""\{0}""", testName),
                @"iisreset",
                @"net start w3svc",
            });
        }

        const string Providers = @"
""Microsoft-Windows-HttpService""   0xFFFFFFFF  4
""Microsoft-Windows-IIS""           0           0
";
        const string PerfCounters = @"""\.NET CLR Exceptions(w3wp)\# of Exceps Thrown / sec""
""\.NET CLR Memory(w3wp)\% Time in GC""
""\Memory\Available MBytes""
""\Memory\Cache Bytes""
""\Memory\Cache Faults/sec""
""\Memory\Demand Zero Faults/sec""
""\Memory\Page Faults/sec""
""\Memory\Pages/sec""
""\Memory\Transition Faults/sec""
""\Network Interface(*)\Bytes Total/sec""
""\Network Interface(*)\Current Bandwidth""
""\PhysicalDisk(_Total)\% Disk Time""
""\PhysicalDisk(_Total)\% Disk Read Time""
""\PhysicalDisk(_Total)\% Disk Write Time""
""\PhysicalDisk(_Total)\Avg. Disk Queue Length""
""\PhysicalDisk(_Total)\Avg. Disk Read Queue Length""
""\PhysicalDisk(_Total)\Avg. Disk Write Queue Length""
""\PhysicalDisk(_Total)\Avg. Disk sec/Read""
""\PhysicalDisk(_Total)\Avg. Disk sec/Write""
""\PhysicalDisk(_Total)\Avg. Disk sec/Transfer""
""\Process(w3wp)\% Processor Time""
""\Process(w3wp)\Handle Count""
""\Process(w3wp)\Thread Count""
""\Process(w3wp)\ID Process""
""\Process(w3wp)\Private Bytes""
""\Process(w3wp)\Virtual Bytes""
""\Process(w3wp)\Working Set""
""\Processor(_Total)\% Interrupt Time""
""\Processor(_Total)\% Privileged Time""
""\Processor(_Total)\% Processor Time""
""\Processor(_Total)\% User Time""
""\System\Context Switches/sec""
""\System\System Calls/sec""
""\Web Service(_Total)\Get Requests/sec""
""\Web Service(_Total)\Post Requests/sec""
""\Web Service(_Total)\Connection Attempts/sec""
""\Web Service(_Total)\Current Connections""
""\Web Service(_Total)\Total Method Requests/sec""
""\Web Service Cache\Kernel: URI Cache Hits %""
""\Web Service Cache\URI Cache Hits %""
";
    }
}
