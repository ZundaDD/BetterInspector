using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 节点数据
/// </summary>
[Serializable]
public class NodeData
{
    public string TypeName;
    public Vector2 Position;
}

/// <summary>
/// 连接数据
/// </summary>
[Serializable]
public class EdgeData
{
    public string InputPort;
    public string OutputPort;
    public int InputNode;
    public int OutputNode;
}
