using Opc.UaFx.Server;

internal static class ServerProgram
{
    private static void Main()
    {
        using var server = new OpcServer("opc.tcp://localhost:4840");
        server.Start();

        Console.WriteLine("Server started. Press any key to stop...");
        Console.ReadKey(true);

        server.Stop();
    }
}
