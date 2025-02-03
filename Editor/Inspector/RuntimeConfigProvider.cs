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
            Label title =
                GetHead(rootElement, "  运行时配置/Runtime Config", true, 20);
            title.style.paddingTop = 2;

            PropertyField localisationPath =
                GetPropertyField(rootElement, "本地化路径", "localizationPath");

            Label headLog =
                GetHead(rootElement, "  输出/Log");

            PropertyField ifLogTaskFinished = 
                GetPropertyField(rootElement, "TaskProgress任务完成时", "ifLogTaskFinished");
        }

        
    }
}