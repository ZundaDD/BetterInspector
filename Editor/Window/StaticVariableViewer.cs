using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 一个包含追踪静态对象的类
    /// </summary>
    public class ClassOfStaticVariable
    {
        public List<SerializedObject> serialOb = new();
        public List<IBaseStaticObject> refs = new();
        public bool foldout = true;
    }

    
    /// <summary>
    /// 静态变量显示器
    /// </summary>
    public class StaticVariableViewer : EditorWindow
    {
        private Dictionary<Type, ClassOfStaticVariable> trackedClasses = new();

        /// <summary>
        /// 显示窗口
        /// </summary>
        [MenuItem("Window/MikanLab/静态成员监视器")]
        public static void ShowWindow()
        {
            GetWindow(typeof(StaticVariableViewer), false, "静态成员监视器");
        }

        /// <summary>
        /// 在激活的时候获取
        /// </summary>
        public void OnEnable()
        {
            //格式大小调整
            minSize = new Vector2(500, 300);
            maxSize = new Vector2(1000, 600);
            EditorGUI.indentLevel++;

            List<System.Type> classesToShow = GetTargetClass();

            foreach (System.Type classType in classesToShow)
            {
                //获取该类下的所有static字段
                FieldInfo[] fields = classType.GetFields(BindingFlags.Static | BindingFlags.GetField | BindingFlags.Public | BindingFlags.NonPublic);
                PropertyInfo[] properties = classType.GetProperties(BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public | BindingFlags.NonPublic);
                MethodInfo[] methods = classType.GetMethods(BindingFlags.Static | BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.NonPublic);

                //根据标签判断是否加入字段
                foreach (FieldInfo field in fields)
                {
                    if (Attribute.IsDefined(field, typeof(EditableStaticAttribute)))
                        trackedClasses[classType].refs.Add(new FieldReference(field, true));

                    if (Attribute.IsDefined(field, typeof(ReadonlyStaticAttribute)))
                        trackedClasses[classType].refs.Add(new FieldReference(field, false));
                }

                //根据标签判断是否加入属性
                foreach (PropertyInfo property in properties)
                {
                    if (Attribute.IsDefined(property, typeof(EditableStaticAttribute)))
                        trackedClasses[classType].refs.Add(new PropertyReference(property, true));

                    if (Attribute.IsDefined(property, typeof(ReadonlyStaticAttribute)))
                        trackedClasses[classType].refs.Add(new PropertyReference(property, false));
                }

                //根据标签以及是否不需要参数判断是否加入方法
                foreach (MethodInfo method in methods)
                {
                    if (method.GetParameters().Length != 0) continue;

                    if (Attribute.IsDefined(method, typeof(VoidStaticMethodAttribute)))
                        trackedClasses[classType].refs.Add(new MethodReference(method));

                }
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
        /// <returns></returns>
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
                    if (Attribute.IsDefined(type, typeof(TrackStaticAttribute)))
                    {
                        classes.Add(type);
                        trackedClasses[type] = new();
                    }
                }
            }
            return classes;
        }

        /// <summary>
        /// 绘制显示内容
        /// </summary>
        private void OnGUI()
        {
            #region 实验区域

            #endregion

            // 绘制警告框
            if (trackedClasses.Count == 0) EditorGUILayout.HelpBox("当前并没有类被追踪", MessageType.Warning);


            foreach (var classItem in trackedClasses)
            {

                GUI.enabled = true;

                //绘制每一个类的Title
                classItem.Value.foldout = EditorGUILayout.Foldout(classItem.Value.foldout, classItem.Key.FullName + " : " + classItem.Value.refs.Count);

                //每一个类具有一个折叠栏
                if (classItem.Value.foldout)
                {

                    EditorGUI.indentLevel++;
                    GUILayout.BeginVertical("box");

                    //绘制每一个属性
                    foreach (var item in classItem.Value.refs) item.Draw();

                    GUILayout.EndVertical();
                    EditorGUI.indentLevel--;
                }

            }
        }
    }
}