using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GUIUtilities
{
    private static GUIStyle boldFoldout;
    public static GUIStyle BoldFoldout
    {
        get
        {
            if (boldFoldout == null)
            {
                boldFoldout = new GUIStyle(EditorStyles.foldout);
                boldFoldout.fontStyle = FontStyle.Bold;
            }
            return boldFoldout;
        }
    }

    public static float currentY;
    public static float spacing = EditorGUIUtility.standardVerticalSpacing;

    public static void BeginVertical(Rect position)
    {
        currentY = position.y;
    }

    public static Rect NextControlRect(Rect position, float height)
    {
        Rect rect = new Rect(position.x, currentY, position.width, height);
        currentY += height + spacing;
        return rect;
    }

}
