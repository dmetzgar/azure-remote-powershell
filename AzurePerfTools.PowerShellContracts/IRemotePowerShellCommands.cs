using System.ServiceModel;

namespace AzurePerfTools.PowerShellContracts
{
    [ServiceContract]
    public interface IRemotePowerShellCommands
    {
        [OperationContract]
        string StartPowerShell();

        [OperationContract]
        string SendCommand(string commandText);
    }
}
