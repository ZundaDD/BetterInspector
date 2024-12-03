using System;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    [CountLimit(NodeType = typeof(Input), Min = 1, Max = 1)]
    [CountLimit(NodeType = typeof(Output),Min = 1, Max = 1)]
    [CreateAssetMenu(fileName = "RandomPool", menuName = "MikanLab/随机池")]
    /// <summary>
    /// 基本随机池
    /// </summary>
    public class RandomPool : NodeGraph, ISerializationCallbackReceiver
    {
        #region 参数
        /// <summary>
        /// 运行时参数表
        /// </summary>
        [NonSerialized] public Dictionary<string, Parameter> Parameters = new();

        /// <summary>
        /// 序列化时参数表
        /// </summary>
        [SerializeField] public List<Parameter> ParametersList = new();
        #endregion

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

            List<int> result = new();


            return result;
        }
        

        #region 序列化
        /// <summary>
        /// 序列化后
        /// </summary>
        public void OnAfterDeserialize()
        {
            Parameters.Clear();
            foreach (var para in ParametersList)
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
        #endregion
    }
}