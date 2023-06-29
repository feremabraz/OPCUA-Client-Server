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
            Console.WriteLine("1. Browse whole server");
            Console.WriteLine("2. Get custom nodes identifiers");
            Console.WriteLine("3. Get a custom node value, given an identifier");
            Console.WriteLine("4. Exit");
            var userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    BrowseServer(node);
                    break;
                case "2":
                    BrowseServer(node, false);
                    break;
                case "3":
                    GetKnownNodeValue(client);
                    break;
                case "4":
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

    private static void GetKnownNodeValue(OpcClient client)
    {
        Console.WriteLine("Enter a OPC UA node identifier for a property.");
        Console.WriteLine("For example: ns=2;s=Production Line 1/SMT Machine 2/ComponentsPerHour");
        string? nodeId = Console.ReadLine();
        OpcReadNode nodeToRead = new OpcReadNode(nodeId);
        OpcValue nodeValue = client.ReadNode(nodeToRead);
        Console.WriteLine("Node: {0}, Value: {1}", nodeId, nodeValue.Value);
    }
    
    private static void BrowseServer(OpcNodeInfo node, bool startPrinting = true, int level = 0)
    {

        if (startPrinting == false && node.NodeId.ToString().Contains("Production Line 1"))
        {
            startPrinting = true;
        }

        if (startPrinting)
        {
            Console.WriteLine("{0}{1}({2})", new string('.', level * 4), node.Attribute(OpcAttribute.DisplayName).Value, node.NodeId);
            startPrinting = true;
        }

        level++;
        foreach (var childNode in node.Children())
        {
            BrowseServer(childNode, startPrinting, level);
        }
    }

}
