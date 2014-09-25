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

        public string StartPowerShell()
        {
            this.Write("\nPS ");
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
            this.Write("\nPS ");
            string output = this.DumpOutput();
            return output;
        }
    }
}
