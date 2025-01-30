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

        public static SerializedProjectSetting GetSettingInstance(Type configType)
        {
            var obj = ScriptableObject.CreateInstance<SerializedProjectSetting>();

            obj.Setting = Activator.CreateInstance(configType) as IProjectSetting;
            var settingStr = PlayerPrefs.GetString(obj.Setting.KeyName, "");
            if (settingStr == "") PlayerPrefs.SetString(obj.Setting.KeyName, JsonUtility.ToJson(obj.Setting));
            else obj.Setting = JsonUtility.FromJson(settingStr,configType) as IProjectSetting;
            
            return obj;
        }

        [SerializeReference]
        public IProjectSetting Setting = null;
    }
}