using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    [TrackStatic]
    public static class RuntimeConfig
    {
        [EditableStatic]
        private static int s;
        [EditableStatic]
        internal static float sdf;
        [EditableStatic]
        public static bool sdfEnabled;
        [EditableStatic]
        public static T a;
    }

    [Serializable]
    public class T
    {
        [SerializeField]public int a;
        [SerializeField]public int v;
    }
}
