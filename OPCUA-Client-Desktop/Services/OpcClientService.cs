using System;
using System.Collections.Generic;
using System.Linq;
using Opc.UaFx;
using Opc.UaFx.Client;

namespace OPCUA_Client_Desktop.Services;

public class OpcClientService : IOpcClientService
{
    private readonly OpcClient _opcClient;
    private OpcNodeInfo? _node;
    private List<OpcNodeData> _fetchResults;

    public OpcClientService()
    {
        _opcClient = new OpcClient("opc.tcp://localhost:4840");
        _opcClient.Connected += (sender, args) => IsConnected = true;
        _opcClient.Disconnected += (sender, args) => IsConnected = false;
        _fetchResults = new List<OpcNodeData>();
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
    
    public List<OpcNodeData> Fetch()
    {
        if (!IsConnected) throw new InvalidOperationException();

        _fetchResults = new List<OpcNodeData>();

        var stack = new Stack<Tuple<OpcNodeInfo, int>>();
        stack.Push(Tuple.Create(_node, 0)!);

        while (stack.Count > 0)
        {
            var (currentNode, level) = stack.Pop();

            var displayName = currentNode?.Attribute(OpcAttribute.DisplayName).Value.ToString() ?? string.Empty;;
            var nodeId = currentNode?.NodeId.ToString() ?? string.Empty;
            var value = currentNode?.AttributeValue(OpcAttribute.Value);
        
            var nodeData = new OpcNodeData(displayName, nodeId, level, value ?? new object());
            _fetchResults.Add(nodeData);

            level++;

            foreach (var childNode in currentNode?.Children() ?? Enumerable.Empty<OpcNodeInfo>())
            {
                stack.Push(Tuple.Create(childNode, level));
            }
        }

        _fetchResults = _fetchResults
            .Where(nodeData => nodeData.NodeId.StartsWith("ns=2"))
            .ToList();

        return _fetchResults;
    }

    public string FetchSingle(string nodeId)
    {
        var nodeValue = _opcClient.ReadNode(nodeId);
        return nodeValue.Value?.ToString() ?? string.Empty;
    }

}
