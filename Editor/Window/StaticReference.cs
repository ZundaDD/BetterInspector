using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 基础静态对象
    /// </summary>
    public interface IBaseStaticObject
    {
        /// <summary>
        /// 是否可编辑
        /// </summary>
        public bool IfEditable { get; set; }

        /// <summary>
        /// 对象名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 绘制自身
        /// </summary>
        public void Draw();

        /// <summary>
        /// 获取对应访问类型的绘制颜色
        /// </summary>
        /// <returns></returns>
        public Color GetAccessColor();

        /// <summary>
        /// 获取对应访问类型的文本
        /// </summary>
        /// <returns></returns>
        public string GetAccessText();
    }

    /// <summary>
    /// 静态字段引用
    /// </summary>
    public class FieldReference : IBaseStaticObject
    {
        public FieldInfo FieldInfo { get; private set; }

        public bool IfEditable { get; set; }

        public Type ValueType { get; set; }

        public FieldReference(FieldInfo fieldInfo, bool editable)
        {
            FieldInfo = fieldInfo;
            ValueType = fieldInfo.FieldType;
            IfEditable = editable;
        }

        public string Name => FieldInfo.Name;

        public object Value => FieldInfo.GetValue(null);

        public void SetValue(object value) => FieldInfo.SetValue(null, value);

        public Color GetAccessColor()
        {
            if (FieldInfo.IsPublic) return Color.gray;
            if (FieldInfo.IsPrivate) return Color.red;
            if (FieldInfo.IsFamily) return Color.cyan;
            if (FieldInfo.IsAssembly) return Color.blue;
            if (FieldInfo.IsFamilyOrAssembly) return Color.green;
            else return Color.magenta;
        }

        public string GetAccessText()
        {
            if (FieldInfo.IsPublic) return "public";
            if (FieldInfo.IsPrivate) return "private";
            if (FieldInfo.IsFamily) return "protected";
            if (FieldInfo.IsAssembly) return "internal";
            if (FieldInfo.IsFamilyOrAssembly) return "protected internal";
            else return "unknown";
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal();
            GUI.enabled = IfEditable;
            if (!Application.isPlaying) GUI.enabled = false;

            //绘制对象访问类型
            GUI.contentColor = GetAccessColor();
            EditorGUILayout.LabelField(GetAccessText(), GUILayout.Width(100));
            GUI.contentColor = Color.white;


            if (ValueType == typeof(int))
            {
                int newValue = EditorGUILayout.IntField(Name, (int)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(float))
            {
                float newValue = EditorGUILayout.FloatField(Name, (float)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(double))
            {
                double newValue = EditorGUILayout.DoubleField(Name, (double)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(long))
            {
                long newValue = EditorGUILayout.LongField(Name, (long)Value);
                SetValue(newValue);
            }

            // 布尔类型
            else if (ValueType == typeof(bool))
            {
                bool newValue = EditorGUILayout.Toggle(Name, (bool)Value);
                SetValue(newValue);
            }

            // 字符串类型
            else if (ValueType == typeof(string))
            {
                string newValue = EditorGUILayout.TextField(Name, (string)Value);
                SetValue(newValue);
            }

            // 枚举类型
            else if (ValueType.IsEnum)
            {
                Enum newValue = EditorGUILayout.EnumPopup(Name, (Enum)Value);
                SetValue(newValue);
            }

            // Unity 特定类型
            else if (ValueType == typeof(Vector2))
            {
                Vector2 newValue = EditorGUILayout.Vector2Field(Name, (Vector2)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Vector3))
            {
                Vector3 newValue = EditorGUILayout.Vector3Field(Name, (Vector3)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Vector4))
            {
                Vector4 newValue = EditorGUILayout.Vector4Field(Name, (Vector4)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Color))
            {
                Color newValue = EditorGUILayout.ColorField(Name, (Color)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Rect))
            {
                Rect newValue = EditorGUILayout.RectField(Name, (Rect)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(AnimationCurve))
            {
                AnimationCurve newValue = EditorGUILayout.CurveField(Name,
                                                                  (AnimationCurve)Value);
                SetValue(newValue);
            }

            // 其他类型
            else
            {

                EditorGUILayout.HelpBox("暂不支持自定义类型绘制",MessageType.Warning);
            }
            GUILayout.EndHorizontal();
        }
    }

    /// <summary>
    /// 静态属性引用
    /// </summary>
    public class PropertyReference : IBaseStaticObject
    {
        public PropertyInfo PropertyInfo { get; private set; }

        public bool IfEditable { get; set; }

        public Type ValueType { get; set; }

        public PropertyReference(PropertyInfo propertyInfo, bool editable)
        {
            PropertyInfo = propertyInfo;
            ValueType = propertyInfo.PropertyType;
            IfEditable = editable;

        }
        public string Name => PropertyInfo.Name;

        public object Value => PropertyInfo.GetValue(null);

        public void SetValue(object value) => PropertyInfo.SetValue(null, value);

        public Color GetAccessColor()
        {
            if (PropertyInfo.SetMethod != null)
            {
                if (PropertyInfo.SetMethod.IsPublic) return Color.gray;
                if (PropertyInfo.SetMethod.IsPrivate) return Color.red;
                if (PropertyInfo.SetMethod.IsFamily) return Color.cyan;
                if (PropertyInfo.SetMethod.IsAssembly) return Color.blue;
                if (PropertyInfo.SetMethod.IsFamilyOrAssembly) return Color.green;
                else return Color.magenta;
            }
            else if (PropertyInfo.GetMethod != null)
            {
                if (PropertyInfo.GetMethod.IsPublic) return Color.gray;
                if (PropertyInfo.GetMethod.IsPrivate) return Color.red;
                if (PropertyInfo.GetMethod.IsFamily) return Color.cyan;
                if (PropertyInfo.GetMethod.IsAssembly) return Color.blue;
                if (PropertyInfo.GetMethod.IsFamilyOrAssembly) return Color.green;
                else return Color.magenta;
            }
            else return Color.green;
        }

        public string GetAccessText()
        {
            //如果可编辑且存在Set访问器，则以Set为主
            if (IfEditable && PropertyInfo.SetMethod != null)
            {
                if (PropertyInfo.SetMethod.IsPublic) return "public set";
                if (PropertyInfo.SetMethod.IsPrivate) return "private set";
                if (PropertyInfo.SetMethod.IsFamily) return "protected set";
                if (PropertyInfo.SetMethod.IsAssembly) return "internal set";
                if (PropertyInfo.SetMethod.IsFamilyOrAssembly) return "protected internal set";
                else return "unknown set";
            }
            else if (PropertyInfo.GetMethod != null)
            {
                if (PropertyInfo.GetMethod.IsPublic) return "public get";
                if (PropertyInfo.GetMethod.IsPrivate) return "private get";
                if (PropertyInfo.GetMethod.IsFamily) return "protected get";
                if (PropertyInfo.GetMethod.IsAssembly) return "internal get";
                if (PropertyInfo.GetMethod.IsFamilyOrAssembly) return "protected internal get";
                else return "unknown get";
            }
            else return "public get/set";
        }

        public void Draw()
        {

            GUILayout.BeginHorizontal();
            GUI.enabled = IfEditable;
            if (!Application.isPlaying) GUI.enabled = false;

            //绘制对象访问类型
            GUI.contentColor = GetAccessColor();
            EditorGUILayout.LabelField(GetAccessText(), GUILayout.Width(100));
            GUI.contentColor = Color.white;


            if (ValueType == typeof(int))
            {
                int newValue = EditorGUILayout.IntField(Name, (int)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(float))
            {
                float newValue = EditorGUILayout.FloatField(Name, (float)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(double))
            {
                double newValue = EditorGUILayout.DoubleField(Name, (double)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(long))
            {
                long newValue = EditorGUILayout.LongField(Name, (long)Value);
                SetValue(newValue);
            }

            // 布尔类型
            else if (ValueType == typeof(bool))
            {
                bool newValue = EditorGUILayout.Toggle(Name, (bool)Value);
                SetValue(newValue);
            }

            // 字符串类型
            else if (ValueType == typeof(string))
            {
                string newValue = EditorGUILayout.TextField(Name, (string)Value);
                SetValue(newValue);
            }

            // 枚举类型
            else if (ValueType.IsEnum)
            {
                Enum newValue = EditorGUILayout.EnumPopup(Name, (Enum)Value);
                SetValue(newValue);
            }

            // Unity 特定类型
            else if (ValueType == typeof(Vector2))
            {
                Vector2 newValue = EditorGUILayout.Vector2Field(Name, (Vector2)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Vector3))
            {
                Vector3 newValue = EditorGUILayout.Vector3Field(Name, (Vector3)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Vector4))
            {
                Vector4 newValue = EditorGUILayout.Vector4Field(Name, (Vector4)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Color))
            {
                Color newValue = EditorGUILayout.ColorField(Name, (Color)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(Rect))
            {
                Rect newValue = EditorGUILayout.RectField(Name, (Rect)Value);
                SetValue(newValue);
            }
            else if (ValueType == typeof(AnimationCurve))
            {
                AnimationCurve newValue = EditorGUILayout.CurveField(Name,
                                                                  (AnimationCurve)Value);
                SetValue(newValue);
            }

            // 其他类型
            else
            {
                EditorGUILayout.LabelField(Name, Value.ToString());
            }
            GUILayout.EndHorizontal();
        }
    }


    public class MethodReference : IBaseStaticObject
    {
        public MethodInfo MethodInfo { get; private set; }

        public bool IfEditable { get; set; }
        public MethodReference(MethodInfo methodInfo)
        {
            MethodInfo = methodInfo;
            IfEditable = true;
        }

        public string Name => MethodInfo.Name;

        public Color GetAccessColor()
        {
            if (MethodInfo.IsPublic) return Color.gray;
            if (MethodInfo.IsPrivate) return Color.red;
            if (MethodInfo.IsFamily) return Color.cyan;
            if (MethodInfo.IsAssembly) return Color.blue;
            if (MethodInfo.IsFamilyOrAssembly) return Color.green;
            else return Color.magenta;
        }

        public string GetAccessText()
        {
            if (MethodInfo.IsPublic) return "public";
            if (MethodInfo.IsPrivate) return "private";
            if (MethodInfo.IsFamily) return "protected";
            if (MethodInfo.IsAssembly) return "internal";
            if (MethodInfo.IsFamilyOrAssembly) return "protected internal";
            else return "unknown";
        }

        public void Draw()
        {
            GUILayout.BeginHorizontal();
            GUI.enabled = IfEditable;
            if (!Application.isPlaying) GUI.enabled = false;

            //绘制对象访问类型
            GUI.contentColor = GetAccessColor();
            EditorGUILayout.LabelField(GetAccessText(), GUILayout.Width(100));
            GUI.contentColor = Color.white;

            EditorGUILayout.LabelField(Name, GUILayout.Width(100));
            if (GUILayout.Button("激活", GUILayout.Width(100)))
            {
                MethodInfo.Invoke(null, null);
            };
            GUILayout.EndHorizontal();
        }
    }
}