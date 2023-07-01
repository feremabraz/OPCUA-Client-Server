using Opc.UaFx.Client;

namespace OPCUA_Client_Desktop.Services;

public class OpcClientService : IOpcClientService
{
    private OpcClient _opcClient;

    public OpcClientService()
    {
        _opcClient = new OpcClient("opc.tcp://localhost:4840");
        _opcClient.Connected += (sender, args) => IsConnected = true;
        _opcClient.Disconnected += (sender, args) => IsConnected = false;
    }

    public bool IsConnected { get; private set; }
    
    public void Connect()
    {
        _opcClient.Connect();
    }

    public void Disconnect()
    {
        _opcClient.Disconnect();
    }
}
