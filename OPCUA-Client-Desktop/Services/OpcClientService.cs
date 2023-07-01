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
        List<string> results = new List<string>();

        Stack<Tuple<OpcNodeInfo, int>> stack = new Stack<Tuple<OpcNodeInfo, int>>();
        stack.Push(Tuple.Create(_node, 0)!);

        while (stack.Count > 0)
        {
            var (currentNode, level) = stack.Pop();

            string result = $"{new string('.', level * 4)}{currentNode?.Attribute(OpcAttribute.DisplayName).Value}({currentNode?.NodeId})";
            results.Add(result);

            level++;

            foreach (var childNode in currentNode?.Children() ?? Enumerable.Empty<OpcNodeInfo>())
            {
                stack.Push(Tuple.Create(childNode, level));
            }
        }

        return results;
    }

}
