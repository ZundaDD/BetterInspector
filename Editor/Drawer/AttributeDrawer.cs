using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    [CustomPropertyDrawer(typeof(BaseAttribute))]
    public class AttributeDrawer : PropertyDrawer
    {
        Texture2D deleteIcon = (Texture2D)AssetDatabase.LoadAssetAtPath("Assets/MyIcon.png", typeof(Texture2D));
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginHorizontal();

            //获取枚举类型
            AttributeType type = (AttributeType) property.FindPropertyRelative("typeEnum").enumValueIndex;
            
            EditorGUILayout.EnumPopup(type,GUILayout.Width(100));
            EditorGUILayout.TextField(property.FindPropertyRelative("name").stringValue,GUILayout.Width(100));
            EditorGUILayout.LabelField(":",GUILayout.Width(20));
            
            //EditorGUILayout.PropertyField(property.FindPropertyRelative("value"),GUILayout.Width(100));
            //if (GUILayout.Button(new GUIContent("删除"),GUILayout.Width(80))) {};
            EditorGUILayout.EndHorizontal();
        }
    }
}