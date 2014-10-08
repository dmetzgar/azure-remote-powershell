using AzurePerfTools.PowerShellContracts;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.Diagnostics.Management;
using Microsoft.WindowsAzure.ServiceRuntime;
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceProcess;

namespace AzurePerfTools.PowerShellWindowsService
{
    public partial class AzureRemotePowerShell : ServiceBase
    {
        ServiceHost remotePowerShellHost;
        private EventLog eventLog;

        public AzureRemotePowerShell()
        {
            InitializeComponent();
            eventLog = new EventLog();
            eventLog.Source = "AzureRemotePowerShell";
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                ConfigureProfileStorage();
            }
            catch (Exception e)
            {
                eventLog.WriteEntry(e.Message, EventLogEntryType.Warning);
            }

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

        private void ConfigureProfileStorage()
        {
            LocalResource profileStorage;
            try
            {
                profileStorage = RoleEnvironment.GetLocalResource("ProfileStorage");
            }
            catch (RoleEnvironmentException e)
            {
                throw new InvalidOperationException("Local storage \"ProfileStorage\" not found.", e);
            }

            var ridm = new RoleInstanceDiagnosticManager(
                CloudConfigurationManager.GetSetting("AzurePerfTools.PowerShellWindowsService.ConnectionString"),
                RoleEnvironment.DeploymentId,
                RoleEnvironment.CurrentRoleInstance.Role.Name,
                RoleEnvironment.CurrentRoleInstance.Id);
            var dmc = ridm.GetCurrentConfiguration() ?? DiagnosticMonitor.GetDefaultInitialConfiguration();
            DirectoryConfiguration directoryConfig;
            
            directoryConfig = new DirectoryConfiguration()
            {
                Container = "profiles",
                Path = profileStorage.RootPath,
                //DirectoryQuotaInMB = 1000,
            };
            dmc.Directories.DataSources.Add(directoryConfig);
            dmc.Directories.ScheduledTransferPeriod = TimeSpan.FromMinutes(1.0);
            ridm.SetCurrentConfiguration(dmc);
        }
    }
}
