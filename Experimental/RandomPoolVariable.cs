using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

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
    public Parameter(string name, object value)
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


/// <summary>
/// 输入节点
/// </summary>
public class InputNode : Node
{
    public List<Parameter> Input;
}

/// <summary>
/// 输出节点
/// </summary>
public class OutputNode : Node
{

}

/// <summary>
/// 待选项节点
/// </summary>
public class ItemNode : Node
{
    public int ID;
    public int Count;
}

/// <summary>
/// 修饰节点
/// </summary>
public class ModifyNode : Node
{

}

/// <summary>
/// 条件节点
/// </summary>
public class ConditionNode : Node
{

}