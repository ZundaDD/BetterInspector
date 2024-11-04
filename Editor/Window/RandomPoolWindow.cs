using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    public class RandomPoolWindow : AssetEditor<RandomPool, RandomPoolWindow>
    {
        SerializedObject curSerailzedPool;

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
            maxSize = new Vector2(500, 800);
            position = new Rect(100, 100, 300, 150);

        }

        public override void OnGUI()
        {
            base.OnGUI();
            if (curEdit == null) return;

            curEdit.CountMode = GUILayout.Toggle(curEdit.CountMode, "有限数量模式", GUILayout.Width(100));   
            
        }
    }
}