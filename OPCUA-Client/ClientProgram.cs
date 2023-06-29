using Opc.UaFx;
using Opc.UaFx.Client;

internal static class ClientProgram
{
    private static void Main()
    {
        using var client = new OpcClient("opc.tcp://localhost:4840");
        client.Connect();
        var node = client.BrowseNode(OpcObjectTypes.ObjectsFolder);
        var exitRequested = false;
        while (!exitRequested) 
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Browse Server");
            Console.WriteLine("2. Get Known Nodes Values");
            Console.WriteLine("3. Exit");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    BrowseServer(node);
                    break;
                case "2":
                    GetKnownNodesValues(client);
                    break;
                case "3":
                    exitRequested = true;
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose 1, 2, or 3.");
                    break;
            }
            Console.WriteLine();
        }
        Console.WriteLine("Exiting.");
    }

    private static void GetKnownNodesValues(OpcClient client)
    {
        OpcReadNode[] nodesToRead =
        {
            new("ns=2;s=Machine/Name"),
            new("ns=2;s=Machine/Status"),
            new("ns=2;s=Machine/Position"),
            new("ns=2;s=Machine/IsActive"),
            new("ns=2;s=Machine/Temperature")
        };
        IEnumerable<OpcValue> nodeValues = client.ReadNodes(nodesToRead).ToList();
        for (var i = 0; i < nodesToRead.Length; i++)
        {
            OpcReadNode node = nodesToRead[i];
            OpcValue value = nodeValues.ElementAt(i);
            OpcNodeId key = node.NodeId;
            object nodeValue = value.Value;
            Console.WriteLine("Key: {0}, Value: {1}", key, nodeValue);
        }
    }

    private static void BrowseServer(OpcNodeInfo node, int level = 0)
    {
        Console.WriteLine("{0}{1}({2})", new string('.', level * 4), node.Attribute(OpcAttribute.DisplayName).Value, node.NodeId);
        level++;
        foreach (var childNode in node.Children()) BrowseServer(childNode, level);
    }
}
