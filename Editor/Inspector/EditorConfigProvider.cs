using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    public class EditorConfigProvider : ProjectSettingProvider<EditorConfig>
    {
        public EditorConfigProvider(string path, SettingsScope scopes = SettingsScope.User)
            : base(path, scopes) { }

        [SettingsProvider]
        public static SettingsProvider CreateProvider()
        {
            var provider = new EditorConfigProvider("Project/MikanLab/EditorConfig", SettingsScope.Project)
            {
                label = "EditorConfig",
                keywords = new HashSet<string>(new[] { "MikanLab" }),
            };
            return provider;
        }

        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            Label title = new Label() { text = "  编辑器配置/Editor Config" };
            title.style.fontSize = 20;
            title.style.paddingTop = 2;
            title.style.unityFontStyleAndWeight = FontStyle.Bold;
            rootElement.Add(title);
        }
    }
}