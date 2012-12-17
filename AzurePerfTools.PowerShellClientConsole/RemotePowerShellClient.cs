using AzurePerfTools.PowerShellContracts;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace AzurePerfTools.PowerShellClientConsole
{
    class RemotePowerShellClient : ClientBase<IRemotePowerShellCommands>, IRemotePowerShellCommands
    {
        public RemotePowerShellClient(Binding binding, EndpointAddress remoteAddress) :
            base(binding, remoteAddress)
        {
        }

        public RemotePowerShellClient(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        public string StartPowerShell(string providers, string perfCounters)
        {
            return base.Channel.StartPowerShell(providers, perfCounters);
        }

        public string SendCommand(string commandText)
        {
            return base.Channel.SendCommand(commandText);
        }
    }
}
