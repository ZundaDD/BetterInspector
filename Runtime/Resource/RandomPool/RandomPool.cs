using System;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    using NodeGraph;

    [CountLimit(NodeType = typeof(Input), Min = 1, Max = 1)]
    [CountLimit(NodeType = typeof(Output),Min = 1, Max = 1)]
    [CreateAssetMenu(fileName = "RandomPool", menuName = "MikanLab/随机池")]
    /// <summary>
    /// 基本随机池
    /// </summary>
    public class RandomPool : NodeGraph.NodeGraph
    {
        /// <summary>
        /// 参数字典
        /// </summary>
        SerializedDictionary<string, Parameter> Parameters = new();

        /// <summary>
        /// 是否为计数不放回模式
        /// </summary>
        public bool CountMode = false;

        /// <summary>
        /// 获取结果
        /// </summary>
        /// <param name="attrs"></param>
        /// <returns>结果列表</returns>
        public List<int> GetResult(Parameter[] paras)
        {
            //检查参数传入
            if (paras == null) throw new ArgumentNullException("Parameters can't be Null!If the Pool Contains no Parameters,Input Empty Array");
            if (paras.Length != Parameters.Count) throw new ArgumentException("Parameters Count don't Match!");
            foreach (var para in paras)
            {
                if (Parameters.ContainsKey(para.Name))
                {
                    if (para.Type != Parameters[para.Name].Type)
                    {
                        throw new Exception("Parameters " + para.Name + " should be " + Parameters[para.Name].Type);
                    }
                    else
                    {
                        Parameters[para.Name].Value = para.Value;
                    }
                }
                else
                {
                    throw new KeyNotFoundException("Parameters " + para.Name + " doesn't Exist!");
                }
            }

            RandomPoolBaseNode root = null;
            foreach(var node in NodeList)
            {
                if(node.GetType() == typeof(Input))
                {
                    root = node as RandomPoolBaseNode;
                    break;
                }
            }

            bool[] visit = new bool[NodeList.Count];
            visit[root.Index] = true;
            return root.Execute(visit);

        }
        

    }
}