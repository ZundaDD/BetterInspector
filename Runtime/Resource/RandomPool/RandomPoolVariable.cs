using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    public enum ParameterType
    {
        Int, Float, Bool
    }

    [Serializable]
    /// <summary>
    /// 参数
    /// </summary>
    public class Parameter
    {
        public string Name;
        public ParameterType Type;
        [NonSerialized] public object Value;
        public Parameter(string name,object value)
        {
            Name = name;
            Value = value;
            Type vtype = value.GetType();
            if (vtype == typeof(int))
            {
                Type = ParameterType.Int;
            }
            else if (vtype == typeof(bool))
            {
                Type = ParameterType.Bool;
            }
            else if (vtype == typeof(float) || vtype == typeof(double))
            {
                Type = ParameterType.Float;
            }
            else throw new Exception("Invalid Parameter Type!");
        }
    }

    [Serializable]
    /// <summary>
    /// 基础节点
    /// </summary>
    public class BaseNode
    {
        public List<BaseNode> Next;
        public int Level;
    }

    /// <summary>
    /// 输入节点
    /// </summary>
    public class InputNode : BaseNode
    {

    }

    /// <summary>
    /// 输出节点
    /// </summary>
    public class OutputNode : BaseNode
    {

    }

    /// <summary>
    /// 待选项节点
    /// </summary>
    public class ItemNode : BaseNode
    {
        public int ID;
        public int Count;
    }

    /// <summary>
    /// 修饰节点
    /// </summary>
    public class ModifyNode : BaseNode
    {

    }

    /// <summary>
    /// 条件节点
    /// </summary>
    public class ConditionNode : BaseNode
    {

    }

}