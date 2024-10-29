using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    [CustomPropertyDrawer(typeof(MultiAttributeResource.Attribute))]
    public class AttributeDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //获取枚举类型
            MultiAttributeResource.Type type = (MultiAttributeResource.Type) property.FindPropertyRelative("typeEnum").enumValueIndex;
            if(type == MultiAttributeResource.Type.Array)
            {

            }
            else
            {
                var rtype = property.FindPropertyRelative("realType");
                var val = property.FindPropertyRelative("value").GetArrayElementAtIndex(0);
                var content = new GUIContent(property.FindPropertyRelative("name").stringValue);
                switch (type)
                {
                    case MultiAttributeResource.Type.String:
                        EditorGUILayout.TextField(content,val.stringValue);break;
                    case MultiAttributeResource.Type.Float:
                        EditorGUILayout.FloatField(content,val.floatValue);break;
                    case MultiAttributeResource.Type.Int:
                        EditorGUILayout.IntField(content, val.intValue);break;
                    case MultiAttributeResource.Type.Bool:
                        EditorGUILayout.Toggle(content,val.boolValue);break;
                        case MultiAttributeResource.Type.Enum:
                        {
                            
                            //EditorGUILayout.EnumPopup(content, val);
                            break;
                        }
                }
                
            }
        }
    }
}