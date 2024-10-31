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
            EditorGUILayout.HelpBox("此处暂时无法查看，若要编辑请在相应面板执行", MessageType.Warning);
            if(GUILayout.Button("在编辑器中打开"))
            {
                MultiAttributeWindow.ShowWindow(serializedObject.targetObject as MultiAttributeResource);
            }
            
        }
    }
}
