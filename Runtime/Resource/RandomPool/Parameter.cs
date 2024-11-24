using System;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;

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

}