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
            EditorGUILayout.HelpBox("此处仅供查看，若要编辑请在相应面板执行", MessageType.Warning);
            if(GUILayout.Button("在编辑器中打开"))
            {
                MultiAttributeWindow.ShowWindow(serializedObject.targetObject as MultiAttributeResource);
            }

            GUI.enabled = false;

            var attirbuteArray = serializedObject.FindProperty("attributes");
            for (int i = 0; i < attirbuteArray.arraySize; i++)
            {
                // 获取数组元素
                SerializedProperty elementProperty = attirbuteArray.GetArrayElementAtIndex(i);

                // 获取 Name 属性
                SerializedProperty nameProperty = elementProperty.FindPropertyRelative("name");

                // 使用 Name 属性的值作为标签绘制数组元素
                EditorGUILayout.PropertyField(elementProperty, new GUIContent(nameProperty.stringValue), true);
            }
        }
    }
}
