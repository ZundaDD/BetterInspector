using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    /// <summary>
    /// 这个类用来显示所有被加载的MikanLab包
    /// </summary>
    public static class MikanLabConfig
    {
        [SettingsProvider]
        public static SettingsProvider CreateProvider()
        {
            return new SettingsProvider("Project/MikanLab", SettingsScope.Project)
            {
                label = "MikanLab",
                activateHandler = (searchText, rootElement) =>
                {
                    Label title = new Label() { text = "  MikanLab" };

                    title.style.fontSize = 20;
                    title.style.paddingTop = 2;
                    title.style.unityFontStyleAndWeight = FontStyle.Bold;
                    rootElement.Add(title);

                    Label content = new Label() { text = "  这里没有内容，详见子界面" };

                    content.style.fontSize = 12;
                    content.style.paddingTop = 2;
                    rootElement.Add(content);
                }
            };
        }
    }
}