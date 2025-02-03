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
        public bool ifLogTaskFinished = true;
        #endregion
        public string KeyName => "MikanLab/RuntimeConfig";
    }
}