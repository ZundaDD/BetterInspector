using MikanLab;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    [Serializable]
    /// <summary>
    /// 最基本的节点图
    /// </summary>
    public class NodeGraph : ScriptableObject
    {
        #region 序列化
        /// <summary>
        /// 节点列表
        /// </summary>
        [SerializeReference] public List<BaseNode> NodeList = new();
        #endregion

        /// <summary>
        /// 满足节点最少数量需求
        /// </summary>
        public virtual void MeetNodeLimit()
        {
            foreach (var attr in GetType().GetCustomAttributes(typeof(CountLimitAttribute), true))
            {
                uint i = 0, tarMin = (attr as CountLimitAttribute).Min;
                string tarName = (attr as CountLimitAttribute).NodeType.AssemblyQualifiedName;
                Type tarType = (attr as CountLimitAttribute).NodeType;


                foreach (var node in NodeList)
                {
                    if (node.GetType().AssemblyQualifiedName == tarName) i++;
                }
                if (i < tarMin)
                {
                    var node = BaseNode.CreateNode(tarType);
                    node.Position = new((tarMin - i) * 100f, 0f);
                    NodeList.Add(node);
                    i++;
                }
            }
        }

        public void OnEnable()
        {
            foreach (var node in NodeList) node.Owner = this;    
        }

        public virtual void Execute() { }
    }
}