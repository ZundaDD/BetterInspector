using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MikanLab
{
    /// <summary>
    /// 模板实例，提供包内自带模板
    /// </summary>
    public static class TemplateInstance
    {
        [MenuItem("Assets/Create/MikanLab/ProjectSetting")]
        public static void CreateProjectSetting()
        {
            TemplateGenerator.CreateCascadeTemplate("ProjectSetting.cs", new() { "#Provider.cs" });
        }

        [MenuItem("Assets/Create/MikanLab/NamespaceEnum")]
        public static void CreateNamespaceEnum() => TemplateGenerator.CreateFromTemplate("NamespaceEnum.cs");

        [MenuItem("Assets/Create/MikanLab/NamespaceSO")]
        public static void CreateNamespaceSO() => TemplateGenerator.CreateFromTemplate("NamespaceSO.cs");

        [MenuItem("Assets/Create/MikanLab/NamespaceMono")]
        public static void CreateNamespaceMono() => TemplateGenerator.CreateFromTemplate("NamespaceMono.cs");
    }
}