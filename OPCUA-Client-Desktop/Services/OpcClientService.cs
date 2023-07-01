using System;
using System.Collections.Generic;
using System.Linq;
using Opc.UaFx;
using Opc.UaFx.Client;

namespace OPCUA_Client_Desktop.Services;

public class OpcClientService : IOpcClientService
{
    private OpcClient _opcClient;
    private OpcNodeInfo? _node;
    private List<OpcNodeData> _fetchResults;

    public OpcClientService()
    {
        _opcClient = new OpcClient("opc.tcp://localhost:4840");
        _opcClient.Connected += (sender, args) => IsConnected = true;
        _opcClient.Disconnected += (sender, args) => IsConnected = false;
    }

    public bool IsConnected { get; private set; }
    
    public void Connect()
    {
        _opcClient.Connect();
        _node = _opcClient.BrowseNode(OpcObjectTypes.ObjectsFolder);
    }

    public void Disconnect()
    {
        _opcClient.Disconnect();
    }
    
    public List<string> Fetch()
    {
        _fetchResults = new List<OpcNodeData>();

        Stack<Tuple<OpcNodeInfo, int>> stack = new Stack<Tuple<OpcNodeInfo, int>>();
        stack.Push(Tuple.Create(_node, 0)!);

        while (stack.Count > 0)
        {
            var (currentNode, level) = stack.Pop();

            string displayName = currentNode?.Attribute(OpcAttribute.DisplayName).Value.ToString() ?? string.Empty;;
            string nodeId = currentNode?.NodeId.ToString() ?? string.Empty;;
        
            OpcNodeData nodeData = new OpcNodeData(displayName, nodeId, level);
            _fetchResults.Add(nodeData);

            level++;

            foreach (var childNode in currentNode?.Children() ?? Enumerable.Empty<OpcNodeInfo>())
            {
                stack.Push(Tuple.Create(childNode, level));
            }
        }

        List<string> displayNames = _fetchResults
            .Where(nodeData => nodeData.NodeId.StartsWith("ns=2"))
            .Select(nodeData => new string(' ', nodeData.Level * 2) + nodeData.DisplayName)
            .ToList();

        return displayNames;
    }

}
