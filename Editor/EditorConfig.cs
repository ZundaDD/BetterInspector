using System;
using System.IO;

namespace MikanLab
{
    /// <summary>
    /// 编辑器配置
    /// </summary>
    [Serializable]
    public class EditorConfig:IProjectSetting
    {
        
        #region 配置内容
        public string localizationPath = "Resources/Localization";
        #endregion
        public string KeyName => "MikanLab/EditorConfig";

    }
}