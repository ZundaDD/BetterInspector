using System;


namespace MikanLab
{

    /// <summary>
    /// 无参的静态方法
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class DebugMethodAttribute : System.Attribute { }

    /// <summary>
    /// 加入调试的类
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class DebugClassAttribute : System.Attribute { }

}