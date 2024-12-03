using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    public class NodeGraphElement : GraphView
    {
        
        protected List<VisualNode> vnodeList = new();
        public NodeGraph target;
        protected SerializedObject s_target;
        protected SerializedProperty s_nodes;
        public Dictionary<Type, int> nodeCache = new();

        //配置部分
        public NodeGraphElement(NodeGraph target) : base()
        {
            target.MeetNodeLimit();
            this.target = target;
            this.s_target = new(target);
            this.s_nodes = s_target.FindProperty("NodeList");
            

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

            for (int i = 0; i < target.NodeList.Count; ++i)
            {
                var node = target.NodeList[i];
                var visualNode = new VisualNode(node,s_nodes.GetArrayElementAtIndex(i));

                AddElement(visualNode);
                if (!nodeCache.ContainsKey(visualNode.Type)) nodeCache.Add(visualNode.Type, 0);
                nodeCache[visualNode.Type]++;
                vnodeList.Add(visualNode);

                visualNode.SetPosition(new Rect(node.Position, new Vector2(100f, 200f)));
            }

            for (int i = 0; i < target.NodeList.Count; ++i)
            {
                foreach (var edges in target.NodeList[i].OutputPorts)
                {
                    Port outputPort = vnodeList[i].outputContainer.Query<Port>().Where(e => e.portName == edges.Key).First();
                    foreach (var edge in edges.Value.Edges)
                    {
                        Port inputPort = vnodeList[edge.TargetIndex].inputContainer.Query<Port>().Where(e => e.portName == edge.TargetPortName).First();


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
        }

        /// <summary>
        /// 保存更改至资源
        /// </summary>
        public void SaveChangeToAsset()
        {
            s_target.Update();
            s_target.ApplyModifiedProperties();

            Dictionary<Node, int> indexs = new();
            target.NodeList.Clear();

            foreach (var node in nodes)
            {
                var preData = (node as VisualNode).Data;
                preData.NodeName = node.title;
                preData.Position = node.GetPosition().position;

                foreach(var outport in preData.OutputPorts) outport.Value.Edges.Clear();
                foreach(var inport in preData.InputPorts) inport.Value.Edges.Clear();

                target.NodeList.Add(preData);
                indexs[node] = target.NodeList.Count - 1;
            }

            foreach (var edge in edges)
            {
                var outdata = new BaseNode.EdgeData();
                var indata = new BaseNode.EdgeData();

                outdata.TargetPortName = edge.input.portName;
                outdata.TargetIndex = indexs[edge.input.node];

                indata.TargetPortName = edge.output.portName;
                indata.TargetIndex = indexs[edge.output.node];

                var outnode = target.NodeList[indexs[edge.output.node]];
                var innode = target.NodeList[indexs[edge.input.node]];

                outnode.OutputPorts[edge.output.portName].Edges.Add(outdata);
                innode.InputPorts[edge.input.portName].Edges.Add(indata);
            }
        }

        public virtual void AddNewNode(Type nodeType)
        {
            //保存之前的更改
            s_target.Update();
            s_target.ApplyModifiedProperties();
            
            //添加新的显示节点到资源文件中
            var newnode = BaseNode.CreateNode(nodeType);
            target.NodeList.Add(newnode);

            //重新序列化
            s_target = new(target);
            s_nodes = s_target.FindProperty("NodeList");
            for (int i = 0; i < vnodeList.Count; ++i)
            {
                vnodeList[i].serializedProperty = s_nodes.GetArrayElementAtIndex(i);
                vnodeList[i].extensionContainer.Clear();
                vnodeList[i].DrawNode();
            }
            
            //添加新的显示节点
            var node = new VisualNode(newnode,s_nodes.GetArrayElementAtIndex(vnodeList.Count));
            AddElement(node);

            //记录类型数量
            if (!nodeCache.ContainsKey(nodeType)) nodeCache.Add(nodeType, 0);
            nodeCache[nodeType]++;
        }

        public virtual void Execute() { }
    }
}