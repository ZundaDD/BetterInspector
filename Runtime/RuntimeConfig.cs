using System.IO;

namespace MikanLab
{
    /// <summary>
    /// 运行时配置
    /// </summary>
    public class RuntimeConfig : IProjectSetting
    {
        #region 配置内容
        public string localizationPath = "Resources/Localization";
        #endregion
        public string KeyName => "MikanLab/RuntimeConfig";
    }
}