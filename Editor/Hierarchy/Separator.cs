using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    [InitializeOnLoad]
    public class SeparatorDrawer : Editor
    {
        static int frameoff = 1;

        static SeparatorDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
        }

        static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
        {
            var go = EditorUtility.InstanceIDToObject(instanceID);

            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (gameObject == null) return;
            if (!gameObject.TryGetComponent(out SeparatorComponent com)) return;

            //绘制区域
            Rect fullRect = selectionRect;
            fullRect.xMin = 31f;
            fullRect.xMax = EditorGUIUtility.currentViewWidth - frameoff;
            fullRect.yMin -= 1;
            fullRect.yMax += 1;

            Rect fillRect = selectionRect;
            fillRect.xMin = 31f + frameoff;
            fillRect.xMax = EditorGUIUtility.currentViewWidth - 2 * frameoff;
            fillRect.yMin -= 1 - frameoff;
            fillRect.yMax += 1 - frameoff;

            Color formColor = GUI.color;

            //准备样式
            GUIStyle boxstyle = new(GUIStyle.none);
            boxstyle.normal.background = GUIUtilities.WhiteBackground;
            boxstyle.normal.textColor = Color.white;
            boxstyle.fontStyle = FontStyle.Bold;
            boxstyle.alignment = TextAnchor.MiddleCenter;


            //绘制边框
            GUI.backgroundColor = Color.gray;
            GUI.Box(fullRect, "", boxstyle);
            GUI.backgroundColor = formColor;

            //绘制填充
            GUI.backgroundColor = com.FillColor;
            GUI.Box(fillRect, com.Text, boxstyle);
            GUI.backgroundColor = formColor;

        }

        [MenuItem("GameObject/MikanLab/分割线")]
        static void CreateSeparator()
        {
            GameObject go = new GameObject();
            go.AddComponent<SeparatorComponent>();
        }
    }
}