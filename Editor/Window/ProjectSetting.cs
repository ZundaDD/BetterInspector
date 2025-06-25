using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
    public class ProjectSettingProvider<TSetting> : SettingsProvider where TSetting : class, IProjectSetting, new()
    {
        protected SerializedProperty settingProperty;

        public ProjectSettingProvider(string path, SettingsScope scopes = SettingsScope.User)
            : base(path, scopes)
        {
            var obj = ProjectSetting.Serialized<TSetting>();
            var sobj = new SerializedObject(obj);
            settingProperty = sobj.FindProperty("Setting");
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            var it = settingProperty.Copy();
            var ed = settingProperty.GetEndProperty();
            var type = settingProperty.managedReferenceValue.GetType();
            
            //遍历绘制
            if (it.NextVisible(true))
            {
                do
                {
                    //获取属性
                    var attrs = GetAttributes(it, type);
                    if (attrs == null) continue;
                    string label = it.name;
                    
                    foreach(var attr in attrs)
                    {
                        if(attr is SectionAttribute)
                        {
                            GetHead(rootElement, (attr as SectionAttribute).title);
                        }
                        else if (attr is LabelAttribute)
                        {
                            label = (attr as LabelAttribute).title;
                        }
                    }

                    //绘制属性框
                    GetPropertyField(rootElement, label, it.name);

                } while (it.NextVisible(false));
            }
        }

        private PropertyAttribute[] GetAttributes(SerializedProperty property, Type parent)
        {
            string fieldName = property.propertyPath.Split(".")[^1];
            object targetObject = property.serializedObject.targetObject;

            var fieldInfo = parent.GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            if (fieldInfo == null) return null;
            return fieldInfo.GetCustomAttributes<PropertyAttribute>(false).ToArray();
        }

        public override void OnDeactivate()
        {
            base.OnDeactivate();
            ProjectSetting.UpdateValue<TSetting>();
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