using MikanLab;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
        MultiAttributeResource curEdit;

        Vector2 scroll;

        Texture deleteIcon;

        /// <summary>
        /// 工具栏打开窗口
        /// </summary>
        [MenuItem("Window/MikanLab/多重属性资源编辑器")] // 设置菜单项路径
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
            //位置大小调整
            minSize = new Vector2(500, 400);
            maxSize = new Vector2(500, 800);
            position = new Rect(100, 100, 300, 150);

            deleteIcon = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("5f3bcd12f441e1f4a84b5a685237064a"));
            
        }

        private void OnDisable()
        {
            if (curEdit == null) return;
            EditorUtility.SetDirty(curEdit);
            AssetDatabase.SaveAssets();
            curEdit = null;
        }

        public void OnGUI()
        {
            int deleteIndex = -1;
            
            curEdit = (MultiAttributeResource) EditorGUILayout.ObjectField("当前编辑对象", curEdit, typeof(MultiAttributeResource),false);

            if (curEdit == null) return;
            
            //工具栏部分
            GUILayout.BeginHorizontal();
            //绘制操作按钮
            if (GUILayout.Button("添加属性", GUILayout.Width(100)))
            {
                GUI.FocusControl("");
                curEdit.attributes.Add(new StringAttribute());
            }

            GUILayout.EndHorizontal();

            scroll =  EditorGUILayout.BeginScrollView(scroll,false,true);
            
            //依次绘制所有属性
            for(int index = 0;index < curEdit.attributes.Count;++index)
            {
                var Item = curEdit.attributes[index];
                EditorGUILayout.BeginHorizontal();
                
                //判断是否涉及到类型转换
                var newType = (AttributeType) EditorGUILayout.EnumPopup(Item.typeEnum, GUILayout.Width(100));
                if(newType != Item.typeEnum)
                {
                    switch (newType)
                    {
                        case AttributeType.String:
                            curEdit.attributes[index] = new StringAttribute();break;
                        case AttributeType.Int:
                            curEdit.attributes[index] = new IntAttribute();break;
                        case AttributeType.Float:
                            curEdit.attributes[index] = new FloatAttribute();break;
                        case AttributeType.Bool:
                            curEdit.attributes[index] = new BoolAttribute();break;
                    }
                }
                
                //绘制名称
                Item.name = EditorGUILayout.TextField(Item.name,GUILayout.Width(100));
                EditorGUILayout.LabelField(":", GUILayout.Width(10));
                
                //绘制实际值
                switch(Item.typeEnum)
                {
                    case AttributeType.String:
                        (Item as StringAttribute).value = EditorGUILayout.TextField((Item as StringAttribute).value, GUILayout.Width(100));
                        break;
                    case AttributeType.Int:
                        (Item as IntAttribute).value = EditorGUILayout.IntField((Item as IntAttribute).value, GUILayout.Width(100));
                        break;
                    case AttributeType.Float:
                        (Item as FloatAttribute).value = EditorGUILayout.FloatField((Item as FloatAttribute).value, GUILayout.Width(100));
                        break;
                    case AttributeType.Bool:
                        (Item as BoolAttribute).value = EditorGUILayout.Toggle((Item as BoolAttribute).value, GUILayout.Width(100));
                        break;
                }

                //删除按钮绘制
                if (GUILayout.Button(new GUIContent("删除",deleteIcon),GUILayout.Width(50),GUILayout.Height(20)))
                {
                    deleteIndex = index;
                }

                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndScrollView();

            //根据状态进行更改
            if (deleteIndex != -1)
            {
                GUI.FocusControl("");
                curEdit.attributes.RemoveAt(deleteIndex);
                Repaint();
            }
            
        }
    }
}
