namespace Server;

using Opc.UaFx;
using Opc.UaFx.Server;

/// <summary>
/// Represents an implementation of a custom OpcNodeManager.
/// </summary>
internal class NodeManager : OpcNodeManager
{
    /// <summary>
    /// Initializes a new instance of the <see cref="NodeManager"/> class.
    /// </summary>
    public NodeManager() : base("http://server/machines")
    {
    }

    /// <summary>
    /// Creates the nodes provided and associated with the node manager.
    /// </summary>
    /// <param name="references">A dictionary used to determine the logical references between
    /// existing nodes (e.g. OPC default nodes) and the nodes provided by the node
    /// manager.</param>
    /// <returns>An enumerable containing the root nodes of the node manager.</returns>
    /// <remarks>This method will be only called once by the server on start up.</remarks>
    protected override IEnumerable<IOpcNode> CreateNodes(OpcNodeReferenceCollection references)
    {
        // Define each custom root nodes.
        // Let's suppose we have only one machine.
        var machineNode = new OpcFolderNode(new OpcName("Machine", this.DefaultNamespaceIndex));
        
        // Add each custom root node to the Objects-Folder (the root of all server nodes).
        references.Add(machineNode, OpcObjectTypes.ObjectsFolder);
        
        // Add custom sub nodes for each of the custom root nodes.
        var nameNode = new OpcDataVariableNode<string>(machineNode, "Name", "Machine 1");
        var statusNode = new OpcDataVariableNode<byte>(machineNode, "Status", 1);
        var positionNode = new OpcDataVariableNode<sbyte>(machineNode, "Position", -1);
        var isActiveNode = new OpcDataVariableNode<bool>(machineNode, "IsActive", true);
        var temperatureNode = new OpcDataVariableNode<double>(machineNode, "Temperature", 18.3);
    
        // Return each custom root nodes using yield return.
        yield return machineNode;
        yield return nameNode;
        yield return statusNode;
        yield return positionNode;
        yield return isActiveNode;
        yield return temperatureNode;
    }
}
