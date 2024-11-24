using System;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 节点数据
    /// </summary>
    [Serializable]
    public class NodeData
    {
        public string TypeName;
        public Vector2 Position;
        public List<EdgeData> Edges = new();
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
}