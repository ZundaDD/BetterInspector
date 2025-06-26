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
        [MenuItem("Template/ProjectSetting")]
        public static void CreateProjectSetting()
        {
            TemplateGenerator.CreateCascadeTemplate("ProjectSetting.cs", new() { "#Provider.cs" });
        }

        [MenuItem("Template/NamespaceMono")]
        public static void CreateNamespaceMono() => TemplateGenerator.CreateFromTemplate("NamespaceMono.cs");
    }
}