using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    /// <summary>
    /// 项目设置UI
    /// </summary>
    /// <typeparam name="TSetting"></typeparam>
    public class ProjectSettingProvider<TSetting> : SettingsProvider where TSetting : class,IProjectSetting, new()
    {
        protected SerializedProperty settingProperty;
        
        public ProjectSettingProvider(string path, SettingsScope scopes = SettingsScope.User)
            : base(path, scopes)
        {
            var obj = SerializedProjectSetting.GetSerialized<TSetting>();
            var sobj = new SerializedObject(obj);
            settingProperty = sobj.FindProperty("Setting");
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            SerializedProjectSetting.UpdateValue<TSetting>();
        }

        protected PropertyField GetPropertyField(VisualElement rootElement, string label, string propertyName)
        {
            PropertyField field = new() { label = label };
            field.BindProperty(settingProperty.FindPropertyRelative(propertyName));
            rootElement.Add(field);
            return field;
        }

        protected Label GetHead(VisualElement rootElement, string label, bool ifBold = true, int fontSize = 16)
        {
            Label labelF = new Label() { text = label };
            labelF.style.fontSize = fontSize;
            labelF.style.unityFontStyleAndWeight = ifBold ? FontStyle.Bold : FontStyle.Normal;
            rootElement.Add(labelF);
            return labelF;
        }
    }

}