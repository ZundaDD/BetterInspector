using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 类型分类
    /// </summary>
    public enum AttributeType
    {
        String, Int, Float, Bool
    }

    [Serializable]
    /// <summary>
    /// 单个属性
    /// </summary>
    public abstract class BaseAttribute
    {

        [SerializeField]
        /// <summary>
        /// 类型枚举
        /// </summary>
        public AttributeType typeEnum;

        [SerializeField]
        /// <summary>
        /// 变量名
        /// </summary>
        public string name;

    }

    [Serializable]
    public class StringAttribute: BaseAttribute
    {
        [SerializeField]
        /// <summary>
        /// 实际值
        /// </summary>
        public string value;

        public StringAttribute()
        {
            typeEnum = AttributeType.String;
            value = string.Empty;
            name = "default";
        }

        public string GetString() => value;
    }

    [Serializable]
    public class FloatAttribute : BaseAttribute
    {
        [SerializeField]
        /// <summary>
        /// 实际值
        /// </summary>
        public float value;

        public FloatAttribute()
        {
            typeEnum = AttributeType.Float;
            value = 0;
            name = "default";
        }

        public float GetFloat() => value;
    }

    [Serializable]
    public class IntAttribute : BaseAttribute
    {
        [SerializeField]
        /// <summary>
        /// 实际值
        /// </summary>
        public int value;

        public IntAttribute()
        {
            typeEnum = AttributeType.Int;
            value = 0;
            name = "default";
        }

        public int GetInt() => value;
    }

    [Serializable]
    public class BoolAttribute : BaseAttribute
    {
        [SerializeField]
        /// <summary>
        /// 实际值
        /// </summary>
        public bool value;

        public BoolAttribute()
        {
            typeEnum = AttributeType.Bool;
            value = false;
            name = "default";
        }

        public bool GetBool() => value;
    }
}


