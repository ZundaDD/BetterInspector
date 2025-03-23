using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab.File
{
    /// <summary>
    /// 文件系统中的文件
    /// </summary>
    public class File : PI
    {

        public File(string baseLoc)
        {
            this.baseLoc = baseLoc;
        }
    }
}