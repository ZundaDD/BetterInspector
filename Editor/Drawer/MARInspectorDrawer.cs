using MikanLab;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    [CustomEditor(typeof(MultiAttributeResource))]
    public class MultiAttributeDrawer : Editor
    {
        public override void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("此处仅供查看，如要编辑请在编辑器中进行！",MessageType.Warning);
            if(GUILayout.Button("在编辑器中打开"))
            {
                MultiAttributeWindow.ShowWindow(serializedObject.targetObject as MultiAttributeResource);
            }

            SerializedProperty ints = serializedObject.FindProperty("ints");
            SerializedProperty bools = serializedObject.FindProperty("bools");
            SerializedProperty strings = serializedObject.FindProperty("strings");
            SerializedProperty floats = serializedObject.FindProperty("floats");
            SerializedProperty orders = serializedObject.FindProperty("orders");

            int intor = 0, boolor = 0, stringor = 0, floator = 0;
            for (int i= 0;i< orders.arraySize;++i)
            {

                EditorGUILayout.PropertyField((AttributeType)orders.GetArrayElementAtIndex(i).enumValueIndex switch
                {
                    AttributeType.String => strings.GetArrayElementAtIndex(stringor++),
                    AttributeType.Bool => bools.GetArrayElementAtIndex(boolor++),
                    AttributeType.Int => ints.GetArrayElementAtIndex(intor++),
                    AttributeType.Float => floats.GetArrayElementAtIndex(floator++),
                    _ => throw new System.Exception("Unsupported Attribute Type")
                });
            }
        }
    }
}
