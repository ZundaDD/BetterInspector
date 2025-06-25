using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    public class RuntimeConfigProvider : ProjectSettingProvider<MikanLabExtConfig>
    {
        public RuntimeConfigProvider(string path, SettingsScope scopes = SettingsScope.User)
            : base(path, scopes) { }

        [SettingsProvider]
        public static SettingsProvider CreateProvider()
        {
            var provider = new RuntimeConfigProvider("Project/MikanLabExtConfig", SettingsScope.Project)
            {
                label = "MikanLabExtConfig",
                keywords = new HashSet<string>(new[] { "MikanLab" }),
            };
            return provider;
        }


        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            Label title =
                GetHead(rootElement, "运行时配置/Runtime Config", true, 20);
            title.style.paddingTop = 2;

            base.OnActivate(searchContext, rootElement);
        }

        
    }
}