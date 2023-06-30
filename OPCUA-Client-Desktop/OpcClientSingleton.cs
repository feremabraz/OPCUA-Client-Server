using System;
using Opc.UaFx.Client;

namespace OPCUA_Client_Desktop
{
    public class OpcClientSingleton
    {
        private static readonly Lazy<OpcClient> instance = new Lazy<OpcClient>(CreateOpcClient);

        public static OpcClient Instance => instance.Value;

        private static OpcClient CreateOpcClient()
        {
            var client = new OpcClient("opc.tcp://localhost:4840");
            client.Connected += (sender, args) => IsConnected = true;
            client.Disconnected += (sender, args) => IsConnected = false;
            return client;
        }

        private OpcClientSingleton()
        {
            // Private constructor to prevent external instantiation
        }
        public static bool IsConnected { get; private set; }
    }
}
