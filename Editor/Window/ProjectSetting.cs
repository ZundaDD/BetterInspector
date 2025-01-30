using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    /// <summary>
    /// 项目设置UI
    /// </summary>
    /// <typeparam name="TSetting"></typeparam>
    public class ProjectSettingProvider<TSetting> : SettingsProvider where TSetting : IProjectSetting, new()
    {
        protected SerializedProperty settingProperty;
        
        public ProjectSettingProvider(string path, SettingsScope scopes = SettingsScope.User)
            : base(path, scopes)
        {
            var obj = SerializedProjectSetting.GetSettingInstance(typeof(TSetting));
            var sobj = new SerializedObject(obj);
            settingProperty = sobj.FindProperty("Setting");
        }
    }

}