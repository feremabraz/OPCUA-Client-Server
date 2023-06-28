using Opc.UaFx.Client;

internal static class ClientProgram
{
    private static void Main()
    {
        using var client = new OpcClient("opc.tcp://localhost:4840");
        client.Connect();

        var node = client.BrowseNode("ns=2;s=SomeNode");
        var value = client.ReadNode(node.NodeId);

        Console.WriteLine($"Value: {value.Value}");
    }
}
