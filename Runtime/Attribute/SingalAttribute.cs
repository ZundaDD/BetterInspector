using System;


namespace MikanLab
{
    /// <summary>
    /// 可编辑的静态字段/属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class EditableStaticAttribute : System.Attribute { }

    /// <summary>
    /// 仅可读的静态字段/属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ReadonlyStaticAttribute : System.Attribute { }

    /// <summary>
    /// 无参的静态方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class VoidStaticMethodAttribute : System.Attribute { }

    /// <summary>
    /// 追踪静态成员的类
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class TrackStaticAttribute : System.Attribute { }

}