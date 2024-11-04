using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// Unity对象编辑器
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AssetEditor<TAsset, TWindow> : EditorWindow where TAsset : UnityEngine.Object where TWindow : AssetEditor<TAsset, TWindow>
    {

        /// <summary>
        /// 当前编辑对象
        /// </summary>
        protected TAsset curEdit = null;

        /// <summary>
        /// 当前选择对象
        /// </summary>
        protected TAsset curRef = null;

        /// <summary>
        /// 当前序列化的对象
        /// </summary>
        protected SerializedObject curSerialized = null;

        /// <summary>
        /// 是否固定编辑对象
        /// </summary>
        private bool FixedObject = true;

        /// <summary>
        /// 是否修改过
        /// </summary>
        protected bool IfEdited = false;

        #region 静态设置
        /// <summary>
        /// 是否需要将被编辑的对象序列化
        /// </summary>
        protected static bool NeedToSerialize { get; set; } = false;

        /// <summary>
        /// 窗口名称
        /// </summary>
        protected static string WindowName { get; set; } = "未设置名字的窗口";

        /// <summary>
        /// 工具栏数量
        /// </summary>
        protected static int ToolBarCount { get; set; } = 1;
        #endregion

        /// <summary>
        /// 带编辑对象的打开窗口
        /// </summary>
        /// <param name="OpenWithObject">带入的对象</param>
        public static void ShowWindow(TAsset OpenWithObject)
        {
            var window = GetWindow<TWindow>();
            window.titleContent = new(WindowName);
            window.curRef = OpenWithObject;

            //复制对象
            if (window.curRef != null)
            {
                window.curEdit = Instantiate(window.curRef);
                window.curEdit.name = window.curRef.name;
                if (NeedToSerialize) window.curSerialized = new(window.curEdit);
            }

            window.FixedObject = true;
        }

        public virtual void OnDisable()
        {
            if (IfEdited)
            {
                if (EditorUtility.DisplayDialog("未保存的更改", "是否保存对目标的更改?", "保存", "丢弃"))
                {
                    SaveCurEdit();
                }
            }
            DestroyImmediate(curEdit);

            curRef = null;
            curEdit = null;
            curSerialized = null;
        }

        /// <summary>
        /// 绘制GUI
        /// </summary>
        public virtual void OnGUI()
        {
            #region 工具栏绘制
            EditorGUILayout.BeginHorizontal();
            float widthAve = Mathf.Min(50, position.width / ToolBarCount);
            if (GUILayout.Button("保存" + (IfEdited ? "*" : ""), EditorStyles.toolbarButton, GUILayout.Width(widthAve), GUILayout.Height(20)))
            {
                IfEdited = false;
                SaveCurEdit();
            }
            ToolbarHook(widthAve, 50f);
            EditorGUILayout.EndHorizontal();
            #endregion

            #region 对象选取
            if (FixedObject)
            {
                GUI.enabled = false;
                EditorGUILayout.ObjectField("当前编辑对象", curRef, typeof(TAsset), false);
                GUI.enabled = true;
            }
            else
            {
                var newcurRef = (TAsset)EditorGUILayout.ObjectField("当前编辑对象", curRef, typeof(TAsset), false);

                //对选择对象进行检查
                if (newcurRef != curRef)
                {

                    //要么保留更改，要么丢弃
                    if (IfEdited)
                    {
                        if (EditorUtility.DisplayDialog("未保存的更改", "是否保存对目标的更改?", "保存", "丢弃"))
                        {
                            SaveCurEdit();
                        }
                    }
                    DestroyImmediate(curEdit);

                    //建立新的引用以及编辑对象
                    curRef = newcurRef;
                    if (curRef != null) curEdit = Instantiate(curRef);
                    if (NeedToSerialize && curRef != null) curSerialized = new(curEdit);
                    else curSerialized = null;
                }
            }
            #endregion
        }

        /// <summary>
        /// 保存当前编辑对象
        /// </summary>
        protected virtual void SaveCurEdit()
        {
            if (curEdit != null && curRef != null)
            {
                if (NeedToSerialize) curSerialized.ApplyModifiedProperties();
                string path = AssetDatabase.GetAssetPath(curRef);
                EditorUtility.CopySerialized(curEdit, curRef);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                IfEdited = false;
            }
        }

        /// <summary>
        /// 工具栏钩子，用于自定义工具栏
        /// </summary>
        public virtual void ToolbarHook(float buttonWidth,float buttonHeight) { }
    }

    
}