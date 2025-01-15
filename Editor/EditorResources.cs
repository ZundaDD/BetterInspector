using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    /// <summary>
    /// 预设好的样式和图片
    /// </summary>
    public static class EditorResources
    {
        private static T Load<T>(string GUID) where T : Object
        {
            return AssetDatabase.LoadAssetAtPath<T>(AssetDatabase.GUIDToAssetPath(GUID));
        }

        private static Texture2D whiteBackground;
        public static Texture2D WhiteBackground
        {
            get
            {
                if(whiteBackground == null)
                {
                    whiteBackground = Load<Texture2D>("fbc5911f88914c54a99fbd71ee019847");
                }
                return whiteBackground;
            }
        }

        private static Texture2D deleteIcon;
        public static Texture2D DeleteIcon
        {
            get
            {
                if(deleteIcon == null)
                {
                    deleteIcon = Load<Texture2D>("5f3bcd12f441e1f4a84b5a685237064a");
                }
                return deleteIcon;
            }
        }

        private static StyleSheet propertyFieldLessenLabel;
        public static StyleSheet PropertyFieldLessenLabel
        {
            get
            {
                if (propertyFieldLessenLabel == null)
                {
                    propertyFieldLessenLabel = Load<StyleSheet>("32a4f7f1be4a4854691c67709e0399e0");
                }
                return propertyFieldLessenLabel;
            }
        }

        private static StyleSheet graphViewColored;
        public static StyleSheet GraphViewColored
        {
            get
            {
                if (graphViewColored == null)
                {
                    graphViewColored = Load<StyleSheet>("47eca1ae3cb39d64682a49e78c79e4c3");
                }
                return graphViewColored;
            }
        }

        private static StyleSheet propertyBox;
        public static StyleSheet PropertyBox
        {
            get
            {
                if (propertyBox == null)
                {
                    propertyBox = Load<StyleSheet>("dcb2cb882dda7f94aa20906a0328910a");
                }
                return propertyBox;
            }
        }
    }
}