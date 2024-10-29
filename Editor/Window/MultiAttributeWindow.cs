using MikanLab;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.PackageManager.UI;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 多重属性资源编辑器
    /// </summary>
    public class MultiAttributeWindow : EditorWindow
    {
        /// <summary>
        /// 当前正在编辑的资源文件
        /// </summary>
        public MultiAttributeResource curEdit;

        /// <summary>
        /// 序列化之后的curEdit
        /// </summary>
        SerializedObject sobj;

        /// <summary>
        /// 是否是第一次打开
        /// </summary>
        bool isFirstOpened = true;

        /// <summary>
        /// 工具栏打开窗口
        /// </summary>
        [MenuItem("Window/多重属性资源编辑器")] // 设置菜单项路径
        public static void ShowWindow()
        {
            GetWindow<MultiAttributeWindow>("多重属性资源编辑器");
        }

        /// <summary>
        /// 带参数的打开窗口
        /// </summary>
        /// <param name="multiAttibuteResource"></param>
        public static void ShowWindow(MultiAttributeResource multiAttibuteResource)
        {
            var window = GetWindow<MultiAttributeWindow>("多重属性资源编辑器");
            window.curEdit = multiAttibuteResource;
        }

        void Awake()
        {
            
            minSize = new Vector2(800, 400);

            // 设置最大尺寸
            maxSize = new Vector2(1200, 800);
            position = new Rect(100, 100, 300, 150);
        }


        private void OnEnable()
        {
            if (curEdit != null) sobj = new(curEdit);
        }

        public void OnGUI()
        {
            if(isFirstOpened && curEdit != null)
            {
                isFirstOpened = false;
                sobj = new(curEdit);
            }
            EditorGUI.BeginChangeCheck();
            
            curEdit = (MultiAttributeResource) EditorGUILayout.ObjectField("当前编辑对象", curEdit, typeof(MultiAttributeResource),false);
            
            //检查引用是否发生变化
            if(EditorGUI.EndChangeCheck())
            {
                Debug.Log("当前编辑对象改变");
                if (curEdit != null) sobj = new(curEdit);
                else sobj = null;
            }
            
            if (sobj == null || curEdit == null) return;

            EditorGUILayout.PropertyField(sobj.FindProperty("attributes"));
            

            sobj.ApplyModifiedProperties();
        }
    }
}
