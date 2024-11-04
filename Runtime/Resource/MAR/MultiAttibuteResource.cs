using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    [Serializable]
    [CreateAssetMenu(fileName = "MAR", menuName = "MikanLab/多属性资源")]
    /// <summary>
    /// 可以存储多种属性的资源文件
    /// </summary>
    public class MultiAttributeResource : ScriptableObject,ISerializationCallbackReceiver
    {
        [NonSerialized] public List<BaseAttribute> attributes = new();
        
        #region 序列化部分
        
        [SerializeField] public List<StringAttribute> strings = new();
        [SerializeField] public List<IntAttribute> ints = new();
        [SerializeField] public List<FloatAttribute> floats = new();
        [SerializeField] public List<BoolAttribute> bools = new();
        [SerializeField] public List<AttributeType> orders = new();

        /// <summary>
        /// 序列化前的转移
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void OnBeforeSerialize()
        {
            strings.Clear();
            ints.Clear();
            floats.Clear();
            bools.Clear();
            orders.Clear();

            foreach (BaseAttribute attribute in attributes)
            {
                switch(attribute.typeEnum)
                {
                    case AttributeType.Bool:
                        bools.Add(attribute as BoolAttribute);
                        orders.Add(AttributeType.Bool);
                        break;
                     case AttributeType.String:
                        strings.Add(attribute as StringAttribute);
                        orders.Add(AttributeType.String);
                        break;
                    case AttributeType.Float:
                        floats.Add(attribute as FloatAttribute);
                        orders.Add(AttributeType.Float);
                        break;
                    case AttributeType.Int:
                        ints.Add(attribute as IntAttribute);
                        orders.Add(AttributeType.Int);
                        break;
                }
            }
        }

        public void OnAfterDeserialize()
        {
            attributes.Clear();
            int intor = 0,stringor = 0,floator = 0,boolor = 0;
            foreach(AttributeType attrType in orders)
            {
                switch(attrType)
                {
                    case AttributeType.Bool:
                        attributes.Add(bools[boolor++]);
                        break;
                    case AttributeType.Float:
                        attributes.Add(floats[floator++]);
                        break;
                    case AttributeType.String:
                        attributes.Add(strings[stringor++]);
                        break;
                    case AttributeType.Int:
                        attributes.Add(ints[intor++]);
                        break;

                }
            }

            strings.Clear();
            ints.Clear();
            floats.Clear();
            bools.Clear();
            orders.Clear();
        }
        #endregion

        #region 访问值部分
        /// <summary>
        /// 获取一个字符串
        /// </summary>
        /// <param name="Name">属性名称</param>
        /// <returns>string值</returns>
        public string GetString(string Name)
        {
            foreach (BaseAttribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == AttributeType.String) return (a as StringAttribute).GetString() ;
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
            foreach (BaseAttribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == AttributeType.Float) return (a as FloatAttribute).GetFloat();
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
            foreach (BaseAttribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == AttributeType.Int) return (a as IntAttribute).GetInt();
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
            foreach (BaseAttribute a in attributes)
            {
                if (a.name == Name)
                {
                    if (a.typeEnum == AttributeType.Bool) return (a as BoolAttribute).GetBool();
                    else throw new System.Exception($"Type of {Name} Dosen't Match {a.typeEnum}");
                }
            }
            throw new System.Exception($"Attibute {Name} Not Found");
        }
        #endregion
    }
}