using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 基础节点
    /// </summary>
    public class BaseNode
    {
        public List<BaseNode> Next;
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
    /// 连接两个节点之间的线
    /// </summary>
    public class Line
    {
        public int Weight; 
    }

    [CreateAssetMenu(fileName ="RandomPool",menuName = "MikanLab/随机池")]
    /// <summary>
    /// 基本随机池
    /// </summary>
    public class RandomPool : ScriptableObject
    {
        /// <summary>
        /// 节点池
        /// </summary>
        public List<BaseNode> NodePool;

        /// <summary>
        /// 是否为计数不放回模式
        /// </summary>
        public bool CountMode = false;

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns>结果列表</returns>
        public List<int> GetResult(params BaseAttribute[] attrs)
        {
            return null;
        }
    }
    
}