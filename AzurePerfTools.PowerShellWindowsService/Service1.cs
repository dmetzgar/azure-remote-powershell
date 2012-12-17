using System.ServiceModel;
using System.ServiceProcess;

namespace AzurePerfTools.PowerShellWindowsService
{
    public partial class AzureRemotePowerShell : ServiceBase
    {
        ServiceHost remotePowerShellHost;

        public AzureRemotePowerShell()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            this.remotePowerShellHost = new ServiceHost(typeof(RemotePowerShellCommands));
            this.remotePowerShellHost.Open();
        }

        protected override void OnStop()
        {
            if (this.remotePowerShellHost != null)
            {
                try
                {
                    this.remotePowerShellHost.Close();
                }
                catch
                {
                    this.remotePowerShellHost.Abort();
                }
            }
        }
    }
}
