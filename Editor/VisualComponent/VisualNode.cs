using System;
using UnityEditor.Experimental.GraphView;

namespace MikanLab
{
    public class VisualNode : Node
    {
        public BaseNode Data;
        public Type Type => Data.GetType();

        public VisualNode(BaseNode data)
        {
            Data = data;
            title = Data.NodeName;
            foreach (var inputPort in data.InputPorts) AddInputPort(inputPort);
            foreach (var outputPort in data.OutputPorts) AddOutputPort(outputPort);
        }

        public void AddInputPort(BaseNode.PortData pd)
        {
            var inputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Input, pd.AllowMultiple ? Port.Capacity.Multi : Port.Capacity.Single, pd.PortType);
            inputPort.portName = pd.PortName;
            inputContainer.Add(inputPort);
        }
        public void AddOutputPort(BaseNode.PortData pd)
        {
            var outputPort = Port.Create<Edge>(Orientation.Horizontal, Direction.Output, pd.AllowMultiple ? Port.Capacity.Multi : Port.Capacity.Single, pd.PortType);
            outputPort.portName = pd.PortName;
            outputContainer.Add(outputPort);
        }
    }
}
