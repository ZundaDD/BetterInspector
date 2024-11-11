using XNode;
using UnityEngine;
using XNodeEditor;
using System;

namespace MikanLab
{
    [CustomNodeGraphEditor(typeof(RandomPool))]
    public class RandomPoolEditor : NodeGraphEditor
    {
        public static void InvokeWindow(RandomPool obj)
        {
            var window = NodeEditorWindow.Open(obj);
            window.titleContent = new($"随机池编辑器 {obj.name}");
        }

        public void OnEnable()
        {

        }

        public override Node CreateNode(Type type, Vector2 position)
        {
            if (type == typeof(InputNode) || type == typeof(OutputNode))
            {
                Debug.LogError("不能重复创建输入/输出节点");
                return null;
            }
            return base.CreateNode(type, position);
        }

        public override void RemoveNode(Node node)
        {
            if (node is InputNode || node is OutputNode)
            {
                Debug.LogError("不能删除输入/输出节点");
                return;
            }
            base.RemoveNode(node);
        }
    }
}