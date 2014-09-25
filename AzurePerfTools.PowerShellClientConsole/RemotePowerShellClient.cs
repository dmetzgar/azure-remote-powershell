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

        public string StartPowerShell()
        {
            return base.Channel.StartPowerShell();
        }

        public string SendCommand(string commandText)
        {
            return base.Channel.SendCommand(commandText);
        }
    }
}
