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
    public class MultiAttributeWindow : AssetEditor<MultiAttributeResource,MultiAttributeWindow>
    {
        static MultiAttributeWindow()
        {
            WindowName = "多重属性资源编辑器";
        }

        /// <summary>
        /// 滚动条值
        /// </summary>
        Vector2 scroll;

        /// <summary>
        /// 删除图标
        /// </summary>
        Texture deleteIcon;

        /// <summary>
        /// 区域限制
        /// </summary>
        GUIStyle FieldStyle = new GUIStyle();
        
        /// <summary>
        /// 工具栏打开窗口
        /// </summary>
        [MenuItem("Window/MikanLab/多重属性资源编辑器")] // 设置菜单项路径
        public static void ShowWindow()
        {
            GetWindow<MultiAttributeWindow>("多重属性资源编辑器");
        }

        void Awake()
        {
            //位置大小调整
            minSize = new Vector2(500, 400);
            maxSize = new Vector2(500, 800);
            position = new Rect(100, 100, 300, 150);

            //加载Icon
            deleteIcon = AssetDatabase.LoadAssetAtPath<Texture>(AssetDatabase.GUIDToAssetPath("5f3bcd12f441e1f4a84b5a685237064a"));
            
            //调整边距
            FieldStyle.margin.top = -4; 
            FieldStyle.margin.bottom = -4;
        }

        public override void OnGUI()
        {
            base.OnGUI();
            if (curEdit == null) return;

            int deleteIndex = -1; 
            
            //工具栏部分
            GUILayout.BeginHorizontal();
            //绘制操作按钮
            if (GUILayout.Button("添加属性", GUILayout.Width(100)))
            {
                GUI.FocusControl("");
                curEdit.attributes.Add(new StringAttribute());
            }

            GUILayout.EndHorizontal();
            
            //表格头部分
            GUILayout.BeginHorizontal();
            GUILayout.Label("类型", GUILayout.Width(100));
            GUILayout.Label("属性名",GUILayout.Width(110));
            GUILayout.Label("属性值", GUILayout.Width(100));


            GUILayout.EndHorizontal();
            EditorGUI.BeginChangeCheck();
            scroll =  GUILayout.BeginScrollView(scroll,false,false);
            

            //依次绘制所有属性
            for(int index = 0;index < curEdit.attributes.Count;++index)
            {
                var Item = curEdit.attributes[index];
                GUILayout.BeginHorizontal(FieldStyle);
                
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
                Item.name = GUILayout.TextField(Item.name,GUILayout.Width(100));
                GUILayout.Label(":", GUILayout.Width(10));
                
                //绘制实际值
                switch(Item.typeEnum)
                {
                    case AttributeType.String:
                        (Item as StringAttribute).value = GUILayout.TextField((Item as StringAttribute).value, GUILayout.Width(100));
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

                GUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
            //如果发生了更改
            if (EditorGUI.EndChangeCheck() && !IfEdited) IfEdited = true;
            

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
