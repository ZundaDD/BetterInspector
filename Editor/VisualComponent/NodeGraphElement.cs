using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    public class NodeGraphElement : GraphView
    {
        protected NodeGraph target;
        public Dictionary<Type, int> nodeCache = new();

        //配置部分
        public NodeGraphElement(NodeGraph target) : base()
        {
            this.target = target;
            target.MeetNodeLimit();

            //交互操作
            this.AddManipulator(new SelectionDragger());
            SetupZoom(0.5f, ContentZoomer.DefaultMaxScale);
            Insert(0, new GridBackground());

            //创建搜索树
            var searchWindowProvider = ScriptableObject.CreateInstance<NodeSeracher>();
            searchWindowProvider.Initialize(this);
            nodeCreationRequest += context =>
            {
                SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), searchWindowProvider);
            };
        }

        public override List<Port> GetCompatiblePorts(Port startAnchor, NodeAdapter nodeAdapter)
        {
            var compatiblePorts = new List<Port>();
            foreach (var port in ports.ToList())
            {
                if (startAnchor.node == port.node ||
                    startAnchor.direction == port.direction ||
                    startAnchor.portType != port.portType)
                {
                    continue;
                }

                compatiblePorts.Add(port);
            }
            return compatiblePorts;
        }

        /// <summary>
        /// 从资源中加载
        /// </summary>
        public void LoadFromAsset()
        {
            List<Node> indexs = new();

            for (int i = 0; i < target.NodeList.Count; ++i)
            {
                var node = target.NodeList[i];
                var visualNode = new VisualNode(node);

                AddElement(visualNode);
                indexs.Add(visualNode);
                if (!nodeCache.ContainsKey(visualNode.Type)) nodeCache.Add(visualNode.Type, 0);
                nodeCache[visualNode.Type]++;

                visualNode.SetPosition(new Rect(node.Position, new Vector2(100f, 200f)));
            }

            for (int i = 0; i < target.NodeList.Count; ++i)
            {
                foreach (var edge in target.NodeList[i].EdgeList)
                {

                    Port inputPort = indexs[edge.TargetIndex].inputContainer.Query<Port>().Where(e => e.portName == edge.TargetPortName).First();
                    Port outputPort = indexs[i].outputContainer.Query<Port>().Where(e => e.portName == edge.ThisPortName).First();

                    if (inputPort != null && outputPort != null)
                    {
                        // 创建边
                        Edge visualEdge = new Edge();

                        // 连接端口
                        visualEdge.input = inputPort;
                        visualEdge.output = outputPort;

                        // 将边添加到 GraphView
                        AddElement(visualEdge);

                        // 更新端口连接状态 (可选，但推荐)
                        inputPort.Connect(visualEdge);
                        outputPort.Connect(visualEdge);
                    }
                    else
                    {
                        throw new("Could not find ports to connect.");
                    }
                }
            }
        }

        /// <summary>
        /// 保存更改至资源
        /// </summary>
        public void SaveChangeToAsset()
        {
            Dictionary<Node, int> indexs = new();
            target.NodeList.Clear();

            foreach (var node in nodes)
            {
                var preData = (node as VisualNode).Data;
                preData.NodeName = node.title;
                preData.Position = node.GetPosition().position;
                preData.EdgeList.Clear();

                target.NodeList.Add(preData);
                indexs[node] = target.NodeList.Count - 1;
            }

            foreach (var edge in edges)
            {
                var data = new BaseNode.EdgeData();

                data.TargetPortName = edge.input.portName;
                data.TargetIndex = indexs[edge.input.node];
                data.ThisPortName = edge.output.portName;

                target.NodeList[indexs[edge.output.node]].EdgeList.Add(data);
            }
        }

        public virtual void Execute() { }
    }
}