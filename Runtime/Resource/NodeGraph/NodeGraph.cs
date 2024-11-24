using MikanLab;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class NodeGraph : ScriptableObject
{
    #region 序列化
    /// <summary>
    /// 节点列表
    /// </summary>
    [SerializeField] public List<NodeData> NodeList = new();
    #endregion

    public virtual void OnEnable()
    {
        MeetNodeLimit();
    }

    /// <summary>
    /// 满足节点最少数量需求
    /// </summary>
    public virtual void MeetNodeLimit()
    {
        foreach (var attr in GetType().GetCustomAttributes(typeof(CountLimitAttribute), true))
        {
            uint i = 0, tar = (attr as CountLimitAttribute).Min;
            string tarName = (attr as CountLimitAttribute).NodeType.AssemblyQualifiedName;

            foreach (var node in NodeList)
            {
                if (node.TypeName == tarName) i++;
            }
            if (i < tar)
            {
                NodeList.Add(new() { TypeName = tarName, Position = new((tar - i) * 100f, 0f) });
                i++;
            }
        }
    }

    public virtual void Execute() { }
}
