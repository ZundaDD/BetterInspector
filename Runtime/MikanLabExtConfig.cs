using System.IO;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 运行时配置
    /// </summary>
    public class MikanLabExtConfig : IProjectSetting
    {
        #region 配置内容
        
        [Section("资源管理"), Label("本地化路径")]
        public string localizationPath = "Resources/Localization";
        
        [Section("时间控制"), Label("TaskProgress任务完成输出")]
        public bool ifLogTaskFinished = true;

        [Section("层级列表"), Label("是否绘制GameObject")]
        public bool ifDrawGameObejct = true;
        [Label("默认绘制颜色")]
        public Color hierarchyFillColor = new Color(0x56 / 256f, 0xb6 / 256f, 0xc0 / 256f);

        #endregion

        public string KeyName => "MikanLabExtConfig";
    }
}