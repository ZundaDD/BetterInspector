using MikanLab;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    [CustomPropertyDrawer(typeof(DecUInt))]
    public class DecUIntDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty frameC = property.FindPropertyRelative("frameCount");
            EditorGUI.PropertyField(position, frameC, label);
        }
    }

    [CustomPropertyDrawer(typeof(DelegateUInt))]
    public class DelegateUIntDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty frameC = property.FindPropertyRelative("frameCount");
            EditorGUI.PropertyField(position, frameC, label);
        }
    }
}