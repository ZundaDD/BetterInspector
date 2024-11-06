using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    public class RandomPoolWindow : AssetEditor<RandomPool, RandomPoolWindow>
    {
        Texture addIcon;
        Texture removeIcon;
        Vector2 pos;
        
        
        /// <summary>
        /// 类配置
        /// </summary>
        static RandomPoolWindow()
        {
            ToolBarCount = 1;
            NeedToSerialize = false;
            WindowName = "随机表编辑器";
        }

        /// <summary>
        /// 工具栏打开窗口
        /// </summary>
        [MenuItem("Window/MikanLab/随机表编辑器")]
        public static void ShowWindow()
        {
            GetWindow<RandomPoolWindow>("随机表编辑器");
        }

        void Awake()
        {
            //位置大小调整
            minSize = new Vector2(500, 400);
            maxSize = new Vector2(1000, 800);
            position = new Rect(100, 100, 300, 150);

            removeIcon = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("bb5dad740c04ab74283bbf37225b1146\r\n"));
            addIcon = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("d68477f066a1ad747b274fea8dfb634e"));
        }

        public override void OnGUI()
        {
            base.OnGUI();
            if (curEdit == null) return;

            EditorGUI.BeginChangeCheck();
            curEdit.CountMode = GUILayout.Toggle(curEdit.CountMode, "有限数量模式", GUILayout.Width(100));

            #region 属性区域
            if (EditorGUI.EndChangeCheck()) IfEdited = true;
            #endregion
            
            #region 编辑器区域
            float w = position.width;
            float h = position.height - 200f;

            //GUILayout.BeginArea(new(5, 100, 200, 200));
            pos = GUILayout.BeginScrollView(pos, false, false);
            

            GUILayout.EndScrollView();
            //GUILayout.EndArea();
            #endregion


            GUILayout.BeginVertical();
            #region 显示节点区域
            
            GUILayout.Button(addIcon,GUILayout.Width( 19),GUILayout.Height(19));
            #endregion
            GUILayout.EndVertical();
        }
    }
}