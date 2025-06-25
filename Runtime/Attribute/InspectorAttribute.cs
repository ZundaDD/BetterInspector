using System;
using UnityEngine;

namespace MikanLab
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class LabelAttribute : PropertyAttribute
    {
        public readonly string title;

        public LabelAttribute(string title)
        {
            this.title = title;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class SectionAttribute : PropertyAttribute
    {
        public readonly string title;

        public SectionAttribute(string title)
        {
            this.title = title;
        }
    }

}