using System.ServiceProcess;

namespace AzurePerfTools.PowerShellWindowsService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
            { 
                new AzureRemotePowerShell() 
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
