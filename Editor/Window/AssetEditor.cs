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
        /// 当前序列化的对象
        /// </summary>
        protected SerializedObject curSerialized = null;

        /// <summary>
        /// 是否固定编辑对象
        /// </summary>
        private bool FixedObject = true;

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
            window.curEdit = OpenWithObject;
            window.FixedObject = true;
        }

        /// <summary>
        /// 绘制GUI
        /// </summary>
        public virtual void OnGUI()
        {
            #region 工具栏绘制
            EditorGUILayout.BeginHorizontal();
            float widthAve = Mathf.Min(50, position.width / ToolBarCount);
            if (GUILayout.Button("保存", EditorStyles.toolbarButton, GUILayout.Width(widthAve), GUILayout.Height(20)))
            {
                SaveCurEdit();
            }
            ToolbarHook(widthAve, 50f);
            EditorGUILayout.EndHorizontal();
            #endregion

            #region 对象选取
            if (FixedObject)
            {
                GUI.enabled = false;
                EditorGUILayout.ObjectField("当前编辑对象", curEdit, typeof(TAsset), false);
                GUI.enabled = true;
            }
            else
            {
                var newcurEdit = (TAsset)EditorGUILayout.ObjectField("当前编辑对象", curEdit, typeof(TAsset), false);

                //对选择对象进行检查
                if (newcurEdit != curEdit)
                {
                    //先保存之前的
                    SaveCurEdit();

                    //建立新的
                    curEdit = newcurEdit;
                    if (NeedToSerialize && curEdit != null) curSerialized = new(curEdit);
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
            if (curEdit == null) return;
            if (NeedToSerialize) curSerialized.ApplyModifiedProperties();
            EditorUtility.SetDirty(curEdit);
            AssetDatabase.SaveAssets();
        }

        /// <summary>
        /// 工具栏钩子，用于自定义工具栏
        /// </summary>
        public virtual void ToolbarHook(float buttonWidth,float buttonHeight) { }
    }

    
}