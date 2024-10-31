using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    [CustomPropertyDrawer(typeof(StringAttribute))]
    public class StringAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            //获取枚举类型
            AttributeType type = (AttributeType) property.FindPropertyRelative("typeEnum").enumValueIndex;
            
            EditorGUILayout.EnumPopup(type,GUILayout.Width(100));
            EditorGUILayout.TextField(property.FindPropertyRelative("name").stringValue,GUILayout.Width(100));
            EditorGUILayout.LabelField(":",GUILayout.Width(20));
            EditorGUILayout.TextField(property.FindPropertyRelative("value").stringValue,GUILayout.Width(100));

            EditorGUILayout.EndHorizontal();
        }
    }
    [CustomPropertyDrawer(typeof(BoolAttribute))]
    public class BoolAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            //获取枚举类型
            AttributeType type = (AttributeType)property.FindPropertyRelative("typeEnum").enumValueIndex;

            EditorGUILayout.EnumPopup(type, GUILayout.Width(100));
            EditorGUILayout.TextField(property.FindPropertyRelative("name").stringValue, GUILayout.Width(100));
            EditorGUILayout.LabelField(":", GUILayout.Width(20));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("value"), GUILayout.Width(100));

            EditorGUILayout.EndHorizontal();
        }
    }

    [CustomPropertyDrawer(typeof(IntAttribute))]
    public class IntAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            //获取枚举类型
            AttributeType type = (AttributeType)property.FindPropertyRelative("typeEnum").enumValueIndex;

            EditorGUILayout.EnumPopup(type, GUILayout.Width(100));
            EditorGUILayout.TextField(property.FindPropertyRelative("name").stringValue, GUILayout.Width(100));
            EditorGUILayout.LabelField(":", GUILayout.Width(20));
            EditorGUILayout.IntField(property.FindPropertyRelative("value").intValue, GUILayout.Width(100));

            EditorGUILayout.EndHorizontal();
        }
    }

    [CustomPropertyDrawer(typeof(FloatAttribute))]
    public class FloatAttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();
            GUI.enabled = false;
            //获取枚举类型
            AttributeType type = (AttributeType)property.FindPropertyRelative("typeEnum").enumValueIndex;

            EditorGUILayout.EnumPopup(type, GUILayout.Width(100));
            EditorGUILayout.TextField(property.FindPropertyRelative("name").stringValue, GUILayout.Width(100));
            EditorGUILayout.LabelField(":", GUILayout.Width(20));
            EditorGUILayout.FloatField(property.FindPropertyRelative("value").floatValue, GUILayout.Width(100));

            EditorGUILayout.EndHorizontal();
        }
    }
}