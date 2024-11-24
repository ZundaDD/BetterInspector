using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace MikanLab
{
    public class NodeGraphElement : GraphView
    {
        protected NodeGraph target;
        public Dictionary<Type, int> nodeCache = new();
        //public RootNode root;

        //配置部分
        public NodeGraphElement(RandomPool target) : base()
        {
            this.target = target;

            //交互操作
            this.AddManipulator(new SelectionDragger());
            SetupZoom(0.5f, ContentZoomer.DefaultMaxScale);
            Insert(0, new GridBackground());

            //创建搜索树
            var searchWindowProvider = new NodeSeracher();
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
                var visualNode = Activator.CreateInstance(Type.GetType(node.TypeName)) as BaseNode;

                AddElement(visualNode);
                indexs.Add(visualNode);
                if (!nodeCache.ContainsKey(visualNode.GetType())) nodeCache.Add(visualNode.GetType(), 0);
                nodeCache[visualNode.GetType()]++;

                visualNode.SetPosition(new Rect(node.Position, new Vector2(100f, 200f)));
            }

            for (int i = 0; i < target.NodeList.Count; ++i)
            {
                foreach (var edge in target.NodeList[i].Edges)
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
                        Debug.LogError("Could not find ports to connect.");
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
                target.NodeList.Add(new() { Position = node.GetPosition().position, TypeName = node.GetType().AssemblyQualifiedName });
                indexs[node] = target.NodeList.Count - 1;
            }

            foreach (var edge in edges)
            {
                var data = new EdgeData();
                data.TargetPortName = edge.input.portName;
                data.TargetIndex = indexs[edge.input.node];
                data.ThisPortName = edge.output.portName;
                target.NodeList[indexs[edge.output.node]].Edges.Add(data);
            }
        }

        public virtual void Execute() { }
    }
}