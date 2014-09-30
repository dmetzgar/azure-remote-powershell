using AzurePerfTools.PowerShellContracts;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;
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
            this.remotePowerShellHost.AddServiceEndpoint(
                typeof(IRemotePowerShellCommands),
                new AzurePerfTools.TableTransportChannel.AzureTableTransportBinding(
                    new TableTransportChannel.AzureTableTransportBindingElement()
                    {
                        ConnectionString = CloudConfigurationManager.GetSetting("AzurePerfTools.PowerShellWindowsService.ConnectionString"),
                        DeploymentId = RoleEnvironment.DeploymentId,
                        RoleName = RoleEnvironment.CurrentRoleInstance.Role.Name,
                        InstanceName = RoleEnvironment.CurrentRoleInstance.Id,
                    }),
                "azure.table:PerfCommands");
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
