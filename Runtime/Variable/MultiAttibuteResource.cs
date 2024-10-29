using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    
    [CreateAssetMenu(fileName = "MAR", menuName = "MikanLab/多属性资源")]
    /// <summary>
    /// 可以存储多种属性的资源文件
    /// </summary>
    public class MultiAttributeResource : ScriptableObject
    {
        [SerializeField]public List<Attribute> attributes = new();

        /// <summary>
        /// 类型分类
        /// </summary>
        public enum Type
        {
            String, Int, Float, Bool, Enum, Array
        }

        [Serializable]
        /// <summary>
        /// 单个属性
        /// </summary>
        public class Attribute
        {
            /// <summary>
            /// 实际类型
            /// </summary>
            public System.Type realType;

            [SerializeField]
            /// <summary>
            /// 类型枚举
            /// </summary>
            public Type typeEnum;

            [SerializeField]
            /// <summary>
            /// 变量名
            /// </summary>
            public string name;

            [SerializeReference]
            /// <summary>
            /// 变量值
            /// </summary>
            public object[] value = new object[] { "1" };


            /// <summary>
            /// 单数构造函数
            /// </summary>
            /// <param name="name">属性名</param>
            /// <param name="value">属性值</param>
            /// <param name="type">属性类型</param>
            /// <exception cref="System.Exception">传入为数组</exception>
            public Attribute(string name,object value,System.Type type)
            {

                //判断数组
                if(type.IsArray) throw new System.Exception("Use the Array Form of Constructor Instead");

                //判断枚举
                if(type.IsEnum)
                {
                    this.typeEnum = Type.Enum;
                    this.realType = type;
                }
                //判断基本类型
                else
                {
                    if (type == typeof(int))
                    {
                        this.realType = typeof(int);
                        this.typeEnum = Type.Int;
                    }
                    else if(type == typeof(float))
                    {
                        this.realType = typeof(float);
                        this.typeEnum = Type.Float;
                    }
                    else if(type == typeof(bool))
                    {
                        this.realType = typeof(bool);
                        this.typeEnum = Type.Bool;
                    }
                    else if(type == typeof(string))
                    {
                        this.realType= typeof(string);
                        this.typeEnum= Type.String;
                    }
                }

                this.name = name;
                this.value = new object[1];
                this.value[0] = value;
            }

            /// <summary>
            /// 数组构造函数
            /// </summary>
            /// <param name="name">属性名</param>
            /// <param name="value">数组值</param>
            /// <param name="type">元数据类型</param>
            /// <exception cref="System.Exception">传入为非数组</exception>
            public Attribute(string name, object[] value, System.Type type)
            {
                if (!type.IsArray) throw new System.Exception("Use the Single Form of Constructor Instead");

                this.name =name;
                this.realType = type.GetElementType();
                typeEnum = Type.Array;
                this.value = value;
            }

        }

        /// <summary>
        /// 不带类型地获取值
        /// </summary>
        /// <param name="Name">属性名称</param>
        /// <returns>值引用</returns>
        public object GetValue(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name) return a.typeEnum == Type.Array ? a.value : a.value[0];
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }

        /// <summary>
        /// 获取一个字符串
        /// </summary>
        /// <param name="Name">属性名称</param>
        /// <returns>string值</returns>
        public string GetString(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == Type.String) return a.value[0] as string;
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }

        /// <summary>
        /// 获取一个float值
        /// </summary>
        /// <param name="Name">属性名称</param>
        /// <returns>float值</returns>
        public float GetFloat(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == Type.Float) return (float) a.value[0];
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }

        /// <summary>
        /// 获取一个int值
        /// </summary>
        /// <param name="Name">属性名称</param>
        /// <returns>int值</returns>
        public int GetInt(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == Type.Int) return (int)a.value[0];
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }

        /// <summary>
        /// 获取一个bool值
        /// </summary>
        /// <param name="Name">属性名称</param>
        /// <returns>bool值</returns>
        public bool GetBool(string Name)
        {
            foreach (Attribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == Type.Bool) return (bool)a.value[0];
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }
    }
}