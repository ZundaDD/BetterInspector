using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 项目设置的接口
    /// </summary>
    public interface IProjectSetting
    {
        public string KeyName { get; }
    }

    /// <summary>
    /// 序列化的项目设置
    /// </summary>
    /// <typeparam name="TSetting">设置对象</typeparam>
    public class SerializedProjectSetting : ScriptableObject
    {
        private static Dictionary<Type,IProjectSetting> insDict = new();

        /// <summary>
        /// 获取序列化的配置
        /// </summary>
        /// <typeparam name="TSetting">配置类型</typeparam>
        /// <returns>序列化配置</returns>
        public static SerializedProjectSetting GetSerialized<TSetting>() where TSetting : class, IProjectSetting, new()
        {
            var obj = ScriptableObject.CreateInstance<SerializedProjectSetting>();
            obj.Setting = GetRaw<TSetting>();
            return obj;
        }

        /// <summary>
        /// 直接访问单例
        /// </summary>
        /// <typeparam name="TSetting">配置类型</typeparam>
        /// <returns>配置单例</returns>
        public static TSetting GetRaw<TSetting>() where TSetting : class,IProjectSetting,new()
        {
            insDict.TryGetValue(typeof(TSetting), out var setting);
            if(setting == null) insDict.Add(typeof(TSetting),FromPrefs<TSetting>());
            return insDict[typeof(TSetting)] as TSetting;
        }

        /// <summary>
        /// 从PlayerPrefs中读取，没有则创建
        /// </summary>
        /// <typeparam name="TSetting">配置类型</typeparam>
        /// <returns>配置实例</returns>
        private static TSetting FromPrefs<TSetting>() where TSetting : class,IProjectSetting,new()
        {
            var ins = new TSetting();
            var prefString = PlayerPrefs.GetString(ins.KeyName, "");
            if (prefString == "") PlayerPrefs.SetString(ins.KeyName, JsonUtility.ToJson(ins));
            else ins = JsonUtility.FromJson<TSetting>(prefString);
            return ins;
        }

        /// <summary>
        /// 更新配置值
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        public static void UpdateValue<TSetting>() where TSetting : class, IProjectSetting, new()
        {
            var ins = GetRaw<TSetting>();
            PlayerPrefs.SetString(ins.KeyName,JsonUtility.ToJson(ins));
            Debug.Log(PlayerPrefs.GetString(ins.KeyName));
        }

        [SerializeReference]
        public IProjectSetting Setting = null;
    }
}