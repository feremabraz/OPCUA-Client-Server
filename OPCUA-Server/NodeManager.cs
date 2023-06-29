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
        // In our case, it would be a folder named "Production Line 1".
        var productionLine1Node = new OpcFolderNode(new OpcName("Production Line 1", this.DefaultNamespaceIndex));
        
        // Add each custom root node to the Objects-Folder (the root of all server nodes).
        references.Add(productionLine1Node, OpcObjectTypes.ObjectsFolder);

        // Add custom sub nodes for each of the custom root nodes.
        // SMT Machine 1 (Model: Yamaha YSM20)
        var smtMachine1Node = new OpcFolderNode(productionLine1Node, "SMT Machine 1");
        var smtMachine1ModelNode = new OpcDataVariableNode<string>(smtMachine1Node, "Model", "Yamaha YSM20");
        var smtMachine1OperatingSpeedNode = new OpcDataVariableNode<int>(smtMachine1Node, "OperatingSpeed", 150);
        var smtMachine1ErrorStatusNode = new OpcDataVariableNode<string>(smtMachine1Node, "ErrorStatus", "No errors.");
        yield return smtMachine1Node;
        yield return smtMachine1ModelNode;
        yield return smtMachine1OperatingSpeedNode;
        yield return smtMachine1ErrorStatusNode;

        // SMT Machine 2 (Model: Panasonic NPM-W2)
        var smtMachine2Node = new OpcFolderNode(productionLine1Node, "SMT Machine 2");
        var smtMachine2ModelNode = new OpcDataVariableNode<string>(smtMachine2Node, "Model", "Panasonic NPM-W2");
        var smtMachine2ComponentsPerHourNode = new OpcDataVariableNode<double>(smtMachine2Node, "ComponentsPerHour", 200.0);
        var smtMachine2RemainingReelQuantityNode = new OpcDataVariableNode<int>(smtMachine2Node, "RemainingReelQuantity", 0);
        yield return smtMachine2Node;
        yield return smtMachine2ModelNode;
        yield return smtMachine2ComponentsPerHourNode;
        yield return smtMachine2RemainingReelQuantityNode;

        // Reflow Oven (Model: BTU Pyramax)
        var reflowOvenNode = new OpcFolderNode(productionLine1Node, "Reflow Oven");
        var reflowOvenModelNode = new OpcDataVariableNode<string>(reflowOvenNode, "Model", "BTU Pyramax");
        var reflowOvenTemperatureProfileNode = new OpcDataVariableNode<double[]>(reflowOvenNode, "TemperatureProfile", Array.Empty<double>());
        var reflowOvenConveyorBeltSpeedNode = new OpcDataVariableNode<double>(reflowOvenNode, "ConveyorBeltSpeed", 0.0);
        yield return reflowOvenNode;
        yield return reflowOvenModelNode;
        yield return reflowOvenTemperatureProfileNode;
        yield return reflowOvenConveyorBeltSpeedNode;

        // AOI Machine (Model: CyberOptics SE500)
        var aoiMachineNode = new OpcFolderNode(productionLine1Node, "AOI Machine");
        var aoiMachineModelNode = new OpcDataVariableNode<string>(aoiMachineNode, "Model", "CyberOptics SE500");
        var aoiMachineDefectsDetectedNode = new OpcDataVariableNode<int>(aoiMachineNode, "DefectsDetected", 0);
        var aoiMachineInspectionStatusNode = new OpcDataVariableNode<string>(aoiMachineNode, "InspectionStatus", "No defects detected.");
        yield return aoiMachineNode;
        yield return aoiMachineModelNode;
        yield return aoiMachineDefectsDetectedNode;
        yield return aoiMachineInspectionStatusNode;
        
        // ICT Machine (Model: Keysight 3070)
        var ictMachineNode = new OpcFolderNode(productionLine1Node, "ICT Machine");
        var ictMachineModelNode = new OpcDataVariableNode<string>(ictMachineNode, "Model", "Keysight 3070");
        var ictMachineTestResultNode = new OpcDataVariableNode<string>(ictMachineNode, "TestResult", "Untested.");
        var ictMachineTestCoveragePercentageNode = new OpcDataVariableNode<double>(ictMachineNode, "TestCoveragePercentage", 0.0);
        yield return ictMachineNode;
        yield return ictMachineModelNode;
        yield return ictMachineTestResultNode;
        yield return ictMachineTestCoveragePercentageNode;

        // Functional Test Station (Model: National Instruments PXI)
        // Note: No specific values were mentioned in the example for this machine.
        var functionalTestStationNode = new OpcFolderNode(productionLine1Node, "Functional Test Station");
        var functionalTestStationModelNode = new OpcDataVariableNode<string>(functionalTestStationNode, "Model", "National Instruments PXI");
        yield return functionalTestStationNode;
        yield return functionalTestStationModelNode;

        // Packaging Machine (Model: Universal Instruments U-flex)
        // Note: No specific values were mentioned in the example for this machine.
        var packagingMachineNode = new OpcFolderNode(productionLine1Node, "Packaging Machine");
        var packagingMachineModelNode = new OpcDataVariableNode<string>(packagingMachineNode, "Model", "Universal Instruments U-flex");
        yield return packagingMachineNode;
        yield return packagingMachineModelNode;

        // Return each custom root nodes using yield return.
        yield return productionLine1Node;
    }
}
