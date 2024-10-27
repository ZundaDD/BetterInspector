using MikanLab;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(DecUInt))]
public class DecVariableDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        SerializedProperty frameC = property.FindPropertyRelative("frameCount");
        EditorGUI.PropertyField(position, frameC, label);
    }
}
