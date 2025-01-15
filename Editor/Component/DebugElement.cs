using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    /// <summary>
    /// 一个包含追踪静态对象的类
    /// </summary>
    public class DebugClass : VisualElement
    {
        List<DebugMethod> Refs;
        
        public DebugClass(List<DebugMethod> refs,Type Class)
        {
            Refs = refs;
            Draw(Class.FullName);
        }

        public void Draw(string classname)
        {
            Foldout fold = new() { text = $"{classname} : {Refs.Count}"};
            foreach(var method in Refs) fold.Add(method);
            Add(fold);
        }
    }

    public class DebugMethod : VisualElement
    {
        public MethodInfo MethodInfo { get; private set; }

        public DebugMethod(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            Draw();
        }

        public string Name => MethodInfo.Name;

        public Color GetAccessColor()
        {
            if (MethodInfo.IsPublic) return Color.gray;
            if (MethodInfo.IsPrivate) return Color.red;
            if (MethodInfo.IsFamily) return Color.cyan;
            else if (MethodInfo.IsAssembly) return Color.blue;
            else return Color.magenta;
        }

        public string GetAccessText()
        {
            if (MethodInfo.IsPublic) return "public";
            if (MethodInfo.IsPrivate) return "private";
            if (MethodInfo.IsFamily) return "protected";
            else if (MethodInfo.IsAssembly) return "internal";
            else return "unknown";
        }

        public void Draw()
        {
            style.flexDirection = FlexDirection.Row;
            style.alignItems = Align.Center;

            Label access = new Label(GetAccessText());
            access.style.color = GetAccessColor();
            access.style.width = 60;
            Add(access);
            
            Label name = new Label(Name);
            name.style.width = 100;
            Add(name);

            Button alert = new Button() { text = "Execute" };
            alert.clicked += () => { if (Application.isPlaying) MethodInfo.Invoke(null, null); } ;
            alert.style.flexGrow = 1;
            Add(alert);

        }
    }
}