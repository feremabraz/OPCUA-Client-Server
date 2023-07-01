namespace OPCUA_Client_Desktop.Services;

public class OpcNodeData
{
    public string DisplayName { get; set; }
    public string NodeId { get; set; }
    public int Level { get; set; }

    public OpcNodeData(string displayName, string nodeId, int level)
    {
        DisplayName = displayName;
        NodeId = nodeId;
        Level = level;
    }
}
