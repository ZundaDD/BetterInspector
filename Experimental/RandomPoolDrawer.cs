using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RandomPool))]
public class RandompoolDrawer : Editor
{
    public override void OnInspectorGUI()
    {
        EditorGUILayout.HelpBox("此处仅供查看，如要编辑请在编辑器中进行！", MessageType.Warning);
        if (GUILayout.Button("在编辑器中打开"))
        {
            RandomPoolWindow.Invoke(serializedObject.targetObject as RandomPool);
        }
        GUI.enabled = false;
        base.OnInspectorGUI();
    }
}
