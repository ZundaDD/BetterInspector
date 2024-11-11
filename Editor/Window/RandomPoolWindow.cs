using XNodeEditor;
using XNode;
using UnityEditor;
using UnityEngine;
using System;

namespace MikanLab
{
    public class RandomPoolWindow : NodeEditorWindow
    {
        #region 打开方式
        public static RandomPoolWindow InvokeWindow(RandomPool graph)
        {
            if (graph == null) return null;
            
            RandomPoolWindow obj = EditorWindow.GetWindow(typeof(RandomPoolWindow), utility: false, "xNode", focus: true) as RandomPoolWindow;
            obj.wantsMouseMove = true;
            obj.graph = graph;
            obj.titleContent = new($"随机池编辑器 {graph.name}");
            return obj;
        }

        [UnityEditor.Callbacks.OnOpenAsset(-1)]
        static bool OnOpenAsset(int instanceID,int lineNumber)
        {
            string assetPath = AssetDatabase.GetAssetPath(instanceID);
            if(AssetDatabase.GetMainAssetTypeAtPath(assetPath) == typeof(RandomPool))
            {
                InvokeWindow(AssetDatabase.LoadAssetAtPath<RandomPool>(assetPath));
                return true;
            }

            return false;
        }
        #endregion
        /*
        void Awake()
        {
            //位置大小调整
            minSize = new Vector2(500, 400);
            maxSize = new Vector2(1000, 800);
            position = new Rect(100, 100, 300, 150);

            removeIcon = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("bb5dad740c04ab74283bbf37225b1146\r\n"));
            addIcon = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("d68477f066a1ad747b274fea8dfb634e"));
        }
        */
        protected override void OnGUI()
        {
            base.OnGUI();
            
            
        }
    }
}