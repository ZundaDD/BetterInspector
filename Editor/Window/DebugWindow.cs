using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{    
    /// <summary>
    /// 静态变量显示器
    /// </summary>
    public class DebugWindow : EditorWindow
    {

        /// <summary>
        /// 显示窗口
        /// </summary>
        [MenuItem("Window/MikanLab/调试器")]
        public static void ShowWindow()
        {
            GetWindow(typeof(DebugWindow), false, "调试器");
        }

        public void OnEnable()
        {
            minSize = new Vector2(260, 300);
            maxSize = new Vector2(260, 1800);
            Draw();

        }
        
        /// <summary>
        /// 绘制窗口
        /// </summary>
        private void Draw()
        {
            List<System.Type> classesToShow = GetTargetClass();
            
            ScrollView scrollView = new ScrollView();
            scrollView.horizontalScrollerVisibility = ScrollerVisibility.Hidden;
            scrollView.verticalScrollerVisibility = ScrollerVisibility.AlwaysVisible;
            scrollView.style.flexGrow = 1;
            rootVisualElement.Add(scrollView);

            foreach (System.Type classType in classesToShow)
            {
                MethodInfo[] methods = classType.GetMethods(BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.NonPublic);
                List<DebugMethod> refs = new();

                //根据标签以及是否不需要参数判断是否加入方法
                foreach (MethodInfo method in methods)
                {
                    if (method.GetParameters().Length != 0) continue;

                    if (Attribute.IsDefined(method, typeof(DebugMethodAttribute)))
                        refs.Add(new DebugMethod(method));

                }

                scrollView.contentContainer.Add(new DebugClass(refs, classType));
            }
        }

        /// <summary>
        /// 在窗口销毁时调用
        /// </summary>
        public void OnDisable()
        {

        }

        /// <summary>
        /// 通过反射获得所有需要追踪的类
        /// </summary>
        /// <returns>追踪类列表</returns>
        public List<System.Type> GetTargetClass()
        {
            //获取当前使用的程序集里面的所有类型
            List<System.Type> classes = new List<System.Type>();
            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();

            //遍历获取定义了追踪属性的类
            foreach (Assembly assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    //如果定义了属性就跟踪
                    if (Attribute.IsDefined(type, typeof(DebugClassAttribute))) classes.Add(type);
                }
            }
            return classes;
        }

    }
}