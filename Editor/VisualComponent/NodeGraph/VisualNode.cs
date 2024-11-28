using System;
using UnityEditor;
using UnityEngine;
using UnityEditor.Experimental.GraphView;

namespace MikanLab
{
    public class VisualNode : Node
    {
        public SerializedProperty serializedProperty;
        public BaseNode Data;
        public Type Type => Data.GetType();

        public VisualNode(BaseNode data,SerializedProperty serializedproperty)
        {
            this.serializedProperty = serializedproperty;
            Data = data;
            title = Data.NodeName;
            if (!Data.Deleteable) capabilities -= Capabilities.Deletable;
            foreach (var inputPort in data.InputPorts) AddInputPort(inputPort);
            foreach (var outputPort in data.OutputPorts) AddOutputPort(outputPort);
            DrawNode();
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

        /// <summary>
        /// 绘制节点
        /// </summary>
        public void DrawNode()
        {
            var drawerType = EditorUtilities.GetNodeDrawers(Data.GetType());
            var newDrawer = Activator.CreateInstance(drawerType, serializedProperty, this) as NodeDrawer;
            newDrawer.OnDrawer();
        }
    }
}
