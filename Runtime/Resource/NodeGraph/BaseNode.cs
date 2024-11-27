using System;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 最基本的节点
    /// </summary>
    [UniversalUsed]
    [Serializable]
    public abstract class BaseNode
    {
        #region 序列化相关
        /// <summary>
        /// 端口数据
        /// </summary>
        [Serializable]
        public class PortData
        {
            public Type PortType;
            public string PortName;
            public bool AllowMultiple;
        }

        /// <summary>
        /// 连接数据
        /// </summary>
        [Serializable]
        public class EdgeData
        {
            public string TargetPortName;
            public string ThisPortName;
            public int TargetIndex;
        }

        [SerializeField] public List<PortData> InputPorts = new();
        [SerializeField] public List<PortData> OutputPorts = new();
        [SerializeField] public string NodeName = "";
        [SerializeField] public List <EdgeData> EdgeList = new();
        [SerializeField] public Vector2 Position = new(0, 0);
        
        public void AddInputPort(Type portType, string portName = "", bool ifMultiple = false)
        {
            if (portName == "") portName = portType.Name;
            foreach (var Port in InputPorts)
            {
                if (Port.PortName == portName)
                {
                    Debug.LogError($"端口名称重复：{portName}，操作中止");
                    return;
                }
            }
            InputPorts.Add(new PortData() { PortName = portName,PortType = portType,AllowMultiple = ifMultiple});
        }

        public void AddOutputPort(Type portType, string portName = "", bool ifMultiple = false)
        {
            if (portName == "") portName = portType.Name;
            foreach (var Port in OutputPorts)
            {
                if (Port.PortName == portName)
                {
                    Debug.LogError($"端口名称重复：{portName}，操作中止");
                    return;
                }
            }
            OutputPorts.Add(new PortData() { PortName = portName, PortType = portType, AllowMultiple = ifMultiple });
        }

        public static BaseNode CreateNode(Type nodeType)
        {
            if (!nodeType.IsSubclassOf(typeof(BaseNode))) return null;
            return Activator.CreateInstance(nodeType) as BaseNode;
        }
        #endregion

        public virtual void Execute() { }
    }
}
