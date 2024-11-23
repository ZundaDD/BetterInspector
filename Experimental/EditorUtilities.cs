using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EditorUtilities
{
    private static Dictionary<System.Type, string> typeName;

    public static string GetTypeQualifiedName(System.Type type)
    {
        if (typeName == null) typeName = new ();

        string name;
        if (!typeName.TryGetValue(type, out name))
        {
            name = type.AssemblyQualifiedName;
            typeName.Add(type, name);
        }
        return name;
    }
}
