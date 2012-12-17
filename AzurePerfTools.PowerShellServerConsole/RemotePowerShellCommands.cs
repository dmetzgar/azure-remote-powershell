using AzurePerfTools.PowerShellContracts;
using AzurePerfTools.PowerShellHost;
using System.ServiceModel;

namespace AzurePerfTools.PowerShellServerConsole
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single)]
    public class RemotePowerShellCommands : PSListenerConsoleSample, IRemotePowerShellCommands
    {
        public RemotePowerShellCommands() :
            base()
        {
        }

        public string StartPowerShell(string providers, string perfCounters)
        {
            this.Write("\nPSConsoleSample: ");
            string output = this.DumpOutput();
            return output;
        }

        public string SendCommand(string commandText)
        {
            if (this.ShouldExit)
            {
                return "Command ignored, PowerShell is shut down";
            }

            this.Execute(commandText);
            this.Write("\nPSConsoleSample: ");
            string output = this.DumpOutput();
            return output;
        }
    }
}
