using System.Linq;
using UnityEngine;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using System;

[Serializable]
public abstract class BaseNode : Node 
{
    public Port AddInputPort(Type portType, string portName = "" ,bool ifMultiple = false)
    {
        if (portName == "") portName = portType.Name;
        foreach (var Port in inputContainer.Children())
        {
            if ((Port as Port).portName == portName)
            {
                Debug.LogError($"端口名称重复：{portName}，操作中止");
                return null;
            }
        }
        var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, ifMultiple?Port.Capacity.Multi : Port.Capacity.Single, portType);
        inputPort.portName = portName;
        inputContainer.Add(inputPort);
        return inputPort;
    }

    public Port AddOutputPort(Type portType, string portName = "", bool ifMultiple = false) 
    {
        if (portName == "") portName = portType.Name;
        foreach (var Port in outputContainer.Children())
        {
            if ((Port as Port).portName == portName)
            {
                Debug.LogError($"端口名称重复：{portName}，操作中止");
                return null;
            }
        }
        var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, ifMultiple ? Port.Capacity.Multi : Port.Capacity.Single, portType);
        outputPort.portName = portName;
        outputContainer.Add(outputPort);
        return outputPort;
    }
}
public abstract class GraphNode : BaseNode
{
    public Port InputPort;
    public Port OutputPort;

    public GraphNode()
    {
        AddInputPort(typeof(string), "In");
        AddOutputPort(typeof(string), "Out");
    }

    public abstract void Execute();
}
public class LogNode : GraphNode
{
    private Port inputString;
    public LogNode() : base()
    {
        title = "Log";

        AddInputPort(typeof(string));
    }
    public override void Execute()
    {
        var edge = inputString.connections.FirstOrDefault();
        var node = edge.output.node as StringNode;

        if (node == null) return;

        Debug.Log(node.Text);
    }
}
public class StringNode : BaseNode
{
    private TextField textField;
    public string Text { get { return textField.value; } }

    public StringNode() : base()
    {
        title = "String";

        AddOutputPort(typeof(string));

        textField = new TextField();
        mainContainer.Add(textField);
    }
}

public class RootNode : BaseNode
{
    public Port OutputPort;

    public RootNode() : base()
    {
        title = "Root";

        capabilities -= Capabilities.Deletable;

        AddOutputPort(typeof(string), "Out");
    }
}
