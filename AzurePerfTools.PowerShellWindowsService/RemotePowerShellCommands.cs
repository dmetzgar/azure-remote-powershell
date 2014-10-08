using AzurePerfTools.PowerShellContracts;
using AzurePerfTools.PowerShellHost;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.ServiceModel;

namespace AzurePerfTools.PowerShellWindowsService
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single, InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
    public class RemotePowerShellCommands : PSListenerConsoleSample, IRemotePowerShellCommands
    {
        public RemotePowerShellCommands() :
            base()
        {
        }

        public string StartPowerShell()
        {
            string roleRoot = Environment.GetEnvironmentVariable("RdRoleRoot");

            // Add path for PerfView
            string perfViewPath = string.Format(@"{0}\approot\bin", roleRoot);
            this.AddToPath(perfViewPath);

            // Set the output directory
            LocalResource profileStorage;
            try
            {
                profileStorage = RoleEnvironment.GetLocalResource("ProfileStorage");
                this.Execute(string.Format(@"$profileStorage = ""{0}""", profileStorage.RootPath));
            }
            catch (RoleEnvironmentException) { }

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

        private void AddToPath(string newPath)
        {
            string before = base.GetCurrentOutput();
            this.Execute(@"$env:Path");
            string after = base.GetCurrentOutput();
            string currPath = after;
            if (!string.IsNullOrEmpty(before))
            {
                after = after.Replace(before, "").Trim();
            }

            if (!currPath.Contains(newPath))
            {
                this.Execute(string.Format(@"$env:Path += "";{0}""", newPath));
            }
        }
    }
}
