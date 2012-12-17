using System.ServiceModel;

namespace AzurePerfTools.PowerShellContracts
{
    [ServiceContract]
    public interface IRemotePowerShellCommands
    {
        [OperationContract]
        string StartPowerShell(string providers, string perfCounters);

        [OperationContract]
        string SendCommand(string commandText);
    }
}
