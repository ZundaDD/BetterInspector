using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    [InitializeOnLoad]
    public class GameObjectDrawer : Editor
    {
        static int frameoff = 1;
        static Object prefabIcon;

        static GameObjectDrawer()
        {
            EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemGUI;
            prefabIcon = EditorGUIUtility.FindTexture("Prefab Icon");
        }

        static void OnHierarchyWindowItemGUI(int instanceID, Rect selectionRect)
        {
            var go = EditorUtility.InstanceIDToObject(instanceID);
            
            GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

            if (gameObject == null) return;
            if (!ProjectSetting.Raw<MikanLabExtConfig>().ifDrawGameObejct) return;
            gameObject.TryGetComponent<HierarchyColorOverride>(out var com);

            //绘制区域
            Rect fullRect = selectionRect;
            fullRect.xMin = selectionRect.x;
            fullRect.xMax = EditorGUIUtility.currentViewWidth - frameoff;
            

            Rect fillRect = selectionRect;
            fillRect.xMin = selectionRect.x + frameoff;
            fillRect.xMax = EditorGUIUtility.currentViewWidth - 2 * frameoff;
            fillRect.yMin += frameoff;
            fillRect.yMax -= frameoff;

            Rect iconRect = selectionRect;
            iconRect.xMin = selectionRect.x + frameoff;
            iconRect.xMax = selectionRect.x + frameoff + 15;
            iconRect.yMin += frameoff;
            iconRect.yMax -= frameoff;

            Color formColor = GUI.color;

            //准备样式
            GUIStyle boxstyle = new(GUIStyle.none);
            boxstyle.normal.background = EditorResources.WhiteBackground;
            boxstyle.normal.textColor = Color.white;
            boxstyle.fontStyle = FontStyle.Bold;
            boxstyle.alignment = TextAnchor.MiddleCenter;


            //绘制边框
            GUI.backgroundColor = Color.gray;
            GUI.Box(fullRect, "", boxstyle);
            GUI.backgroundColor = formColor;

            //绘制填充
            if (com != null) GUI.backgroundColor = com.FillColor;
            else GUI.backgroundColor = ProjectSetting.Raw<MikanLabExtConfig>().hierarchyFillColor;
            GUI.Box(fillRect, gameObject.name, boxstyle);
            GUI.backgroundColor = formColor;

            //绘制预制符号
            if(PrefabUtility.IsPartOfAnyPrefab(gameObject))
                GUI.DrawTexture(iconRect, prefabIcon as Texture);
        }

    }
}

