using Opc.UaFx;
using Opc.UaFx.Client;

internal static class ClientProgram
{
    /// <summary>
    /// Connects to an OPC server, provides a menu for browsing and retrieving node values,
    /// and disconnects from the server upon exit.
    /// </summary>
    private static void Main()
    {
        using var client = new OpcClient("opc.tcp://localhost:4840");
        try
        {
            client.Connect();
        }
        catch (Exception)
        {
            Console.WriteLine($"Failed to connect to the OPC server. Did you forgot to start the server?");
            Environment.Exit(1);
        }
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
        client.Disconnect();
    }

    /// <summary>
    /// Retrieves the value of a known OPC UA node identified by its node identifier.
    /// </summary>
    /// <param name="client">The OPC UA client.</param>
    private static void GetKnownNodeValue(OpcClient client)
    {
        Console.WriteLine("Enter a OPC UA node identifier for a property.");
        Console.WriteLine("For example: ns=2;s=Production Line 1/SMT Machine 2/ComponentsPerHour");

        // Create an OpcReadNode object with the specified node identifier
        string? nodeId = Console.ReadLine();
        OpcReadNode nodeToRead = new OpcReadNode(nodeId);
        
        // Read the value of the node from the OPC UA server and print it down
        OpcValue nodeValue = client.ReadNode(nodeToRead);
        Console.WriteLine("Node: {0}, Value: {1}", nodeId, nodeValue.Value);
    }

    /// <summary>
    /// Recursively browses the OPC UA server starting from the specified node,
    /// printing the node information with indentation.
    /// </summary>
    /// <param name="node">The starting node to browse.</param>
    /// <param name="startPrinting">Determines if printing should start for the current node and its children.</param>
    /// <param name="level">The current level of indentation.</param>
    private static void BrowseServer(OpcNodeInfo node, bool startPrinting = true, int level = 0)
    {

        // Check if printing should start based on the node ID
        if (startPrinting == false && node.NodeId.ToString().Contains("Production Line 1"))
        {
            startPrinting = true;
        }

        // Print node information if printing is allowed for the current node
        if (startPrinting)
        {
            // Construct the formatted string with indentation, display name, and node ID
            // This creates a string consisting of a specified character ('.') repeated a certain number of times.
            // The purpose of this is to add indentation to the string based on the current level. Each level corresponds to a nested node.
            Console.WriteLine("{0}{1}({2})", new string('.', level * 4), node.Attribute(OpcAttribute.DisplayName).Value, node.NodeId);
            startPrinting = true;
        }

        // Recursively browse the child nodes
        level++;
        foreach (var childNode in node.Children())
        {
            BrowseServer(childNode, startPrinting, level);
        }
    }

}
