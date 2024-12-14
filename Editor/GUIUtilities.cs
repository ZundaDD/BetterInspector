using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public static class GUIUtilities
{
    private static StyleSheet propertyFieldLessenLabel;
    public static StyleSheet PropertyFieldLessenLabel
    {
        get
        {
            if(propertyFieldLessenLabel == null)
            {
                propertyFieldLessenLabel = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath("32a4f7f1be4a4854691c67709e0399e0"));
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
                graphViewColored = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath("47eca1ae3cb39d64682a49e78c79e4c3"));
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
                propertyBox = AssetDatabase.LoadAssetAtPath<StyleSheet>(AssetDatabase.GUIDToAssetPath("dcb2cb882dda7f94aa20906a0328910a"));
            }
            return propertyBox;
        }
    }
}
