using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    public class RuntimeConfigProvider : ProjectSettingProvider<RuntimeConfig>
    {
        public RuntimeConfigProvider(string path, SettingsScope scopes = SettingsScope.User)
            : base(path, scopes) { }

        [SettingsProvider]
        public static SettingsProvider CreateProvider()
        {
            var provider = new RuntimeConfigProvider("Project/MikanLab/RuntimeConfig", SettingsScope.Project)
            {
                label = "RuntimeConfig",
                keywords = new HashSet<string>(new[] { "MikanLab" }),
            };
            return provider;
        }


        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            Label title = new Label() { text = "  运行时配置/Runtime Config" };

            title.style.fontSize = 20;
            title.style.paddingTop = 2;
            title.style.unityFontStyleAndWeight = FontStyle.Bold;
            rootElement.Add(title);

            PropertyField localisationPath = new() {label = "本地化路径"};
            localisationPath.BindProperty(settingProperty.FindPropertyRelative("localizationPath"));
            rootElement.Add(localisationPath);
        }

    }
}