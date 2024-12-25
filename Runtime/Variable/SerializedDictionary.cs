using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 序列化字典
    /// </summary>
    /// <typeparam name="TKey">键类型</typeparam>
    /// <typeparam name="TValue">值类型</typeparam>
    [Serializable]
    public class SerializedDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField] private List<TKey> keys;
        [SerializeField] private List<TValue> values;

        public void OnAfterDeserialize()
        {
            Clear();
            for (int i = 0; i < keys.Count; i++)
            {
                Add(keys[i], values[i]);
            }
            keys.Clear();
            keys = null;
            values.Clear();
            values = null;
        }

        public void OnBeforeSerialize()
        {
            keys = new();
            values = new();
            foreach (var keypair in this)
            {
                keys.Add(keypair.Key);
                values.Add(keypair.Value);
            }
        }
    }

}