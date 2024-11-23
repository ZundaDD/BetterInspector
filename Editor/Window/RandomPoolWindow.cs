using XNodeEditor;
using XNode;
using UnityEditor;
using UnityEngine;
using System;
using static UnityEngine.GraphicsBuffer;

namespace MikanLab
{
    /*
    public class RandomPoolWindow : NodeEditorWindow
    {
        #region 打开方式
        public static RandomPoolWindow InvokeWindow(RandomPool graph)
        {
            if (graph == null) return null;

            RandomPoolWindow obj = EditorWindow.GetWindow(typeof(RandomPoolWindow), utility: false, "xNode", focus: true) as RandomPoolWindow;
            obj.wantsMouseMove = true;
            obj.graph = graph;

            return obj;
        }

        public static NodeEditorWindow OpenAsset(NodeGraph graph)
        {
            if (!graph)
            {
                return null;
            }

            RandomPoolWindow obj = EditorWindow.GetWindow(typeof(RandomPoolWindow), utility: false, "xNode", focus: true) as RandomPoolWindow;
            obj.wantsMouseMove = true;
            obj.graph = graph;
            obj.titleContent = new($"随机池编辑器 {graph.name}");

            return obj;
        }

        [UnityEditor.Callbacks.OnOpenAsset(-1)]
        static bool OnOpenAsset(int instanceID, int lineNumber)
        {
            RandomPool nodeGraph = EditorUtility.InstanceIDToObject(instanceID) as RandomPool;
            if (nodeGraph != null)
            {
                OpenAsset(nodeGraph);
                return true;
            }
            return false;
        }
        #endregion

        protected override void OnGUI()
        {
            base.OnGUI();
            GUILayout.Label("a");
        }
    }*/
}