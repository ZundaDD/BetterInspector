using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace MikanLab
{
    [GraphDrawer(typeof(BaseNode))]
    public class NodeDrawer
    {
        protected SerializedProperty property;
        protected VisualElement container;
        public NodeDrawer(SerializedProperty nodeProperty, VisualNode visualnode)
        {
            container = visualnode.extensionContainer;
            property = nodeProperty;
        }
        public virtual void OnDrawer() { }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false,Inherited = false)]
    public class GraphDrawerAttribute : Attribute
    {
        public Type Type;
        public GraphDrawerAttribute(Type type) => this.Type = type;
    }
}