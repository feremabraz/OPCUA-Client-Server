namespace Server;

using Opc.UaFx.Server;

internal static class ServerProgram
{
    private static void Main()
    {
        new OpcServerApplication("opc.tcp://localhost:4840/server", new NodeManager()).Run();
    }
}
