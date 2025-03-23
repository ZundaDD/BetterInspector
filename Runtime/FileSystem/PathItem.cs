using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MikanLab.File
{
    /// <summary>
    /// 文件系统的基本单位
    /// </summary>
    public abstract class PI
    {
        protected string baseLoc;


        private static PI resources;
        public static PI Resources
        {
            get
            {
                if(resources == null)
                {
                    resources = new Folder(Path.Combine(Application.dataPath,"Resources"));
                }
                return resources;
            }
        }


        /// <summary>
        /// 获取子对象
        /// </summary>
        /// <param name="dir">相对路径</param>
        /// <returns>路径项</returns>
        /// <exception cref="System.Exception">文件被打开|路径不存在</exception>
        public PI this[string dir]
        {
            get
            {
                if (this is File) throw new System.Exception("File can't be opened as Folder!");

                string newLoc = Path.Combine(baseLoc, dir);
                if (Directory.Exists(newLoc))
                {
                    return new Folder(newLoc);
                }

                if (System.IO.File.Exists(newLoc))
                {
                    return new File(newLoc);
                }

                throw new System.Exception($"Path {newLoc} doesn't exist!");
            }
        }

        public T As<T>() where T : ScriptableObject
        {
            if (this is Folder) throw new System.Exception("Folder can't be loaded as File!");

            var obj = AssetDatabase.LoadAssetAtPath<T>(baseLoc);
            
            if (obj == null) throw new System.Exception($"File can't be decoded as {typeof(T).Name}");
            return obj;
        }

        public List<Folder> SubFolders
        {
            get
            {
                List<Folder> list = new List<Folder>();
                foreach (var path in Directory.GetDirectories(baseLoc))
                {
                    list.Add(new Folder(path));
                }
                return list;
            }
        }

        public List<File> Files
        {
            get
            {
                List<File> list = new List<File>();
                foreach (var path in Directory.GetFiles(baseLoc))
                {
                    list.Add(new File(path));
                }
                return list;
            }
        }

    }

}