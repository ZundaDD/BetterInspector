using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 游戏设置通用项
    /// </summary>
    public class GameSetting : ScriptableObject
    {
        /// <summary>
        /// 一条配置
        /// </summary>
        [Serializable]
        public class Config
        {
            public string TypeName;
            public string ConfigName;
        }

    }
}