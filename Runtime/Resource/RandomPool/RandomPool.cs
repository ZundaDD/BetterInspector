using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace MikanLab
{
    [CreateAssetMenu(fileName = "RandomPool", menuName = "MikanLab/随机池")]
    /// <summary>
    /// 基本随机池
    /// </summary>
    public class RandomPool :NodeGraph,ISerializationCallbackReceiver
    {
        /// <summary>
        /// 运行时参数表
        /// </summary>
        [NonSerialized] public Dictionary<string, Parameter> Parameters = new();

        /// <summary>
        /// 序列化时参数表
        /// </summary>
        [SerializeField] public List<Parameter> ParametersList = new();

        /// <summary>
        /// 是否为计数不放回模式
        /// </summary>
        public bool CountMode = false;

        /// <summary>
        /// 图有效性检测
        /// </summary>
        private void OnEnable()
        {
            
            bool ifInput = false, ifOutput = false;
            for (int i = 0; i < nodes.Count; ++i)
            {
                var node = nodes[i];
                if (node is InputNode)
                {
                    if (!ifInput) ifInput = true;
                    else
                    {
                        Debug.LogError("图存在多个输入节点，仅保留第一个");
                        RemoveNode(node);
                        i--;
                    }
                }
                if (node is OutputNode)
                {
                    if (!ifOutput) ifOutput = true;
                    else
                    {
                        Debug.LogError("图存在多个输出节点，仅保留第一个");
                        RemoveNode(node);
                        i--;
                    }
                }
            }
            if (!ifInput)
            {
                var node = AddNode(typeof(InputNode));
                node.name = "Input";
            }
            if (!ifOutput)
            {
                var node = AddNode(typeof(OutputNode));
                node.name = "Output";
            }
        }

        
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
                if(Parameters.ContainsKey(para.Name))
                {
                    if(para.Type != Parameters[para.Name].Type)
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

            List<int> result = new();


            return result;
        }

        /// <summary>
        /// 序列化后
        /// </summary>
        public void OnAfterDeserialize()
        {
            Parameters.Clear();
            foreach(var para in ParametersList)
            {
                Parameters[para.Name] = para;
            }
            ParametersList.Clear();
        }

        /// <summary>
        /// 序列化前
        /// </summary>
        public void OnBeforeSerialize()
        {
            ParametersList.Clear();
            foreach (var para in Parameters)
            {
                ParametersList.Add(para.Value);
            }
            Parameters.Clear();
        }
    }
    
}