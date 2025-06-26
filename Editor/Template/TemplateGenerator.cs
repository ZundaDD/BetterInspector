using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.Rendering;

namespace MikanLab
{
    /// <summary>
    /// 模板生成器
    /// </summary>
    public class TemplateGenerator
    {

        /// <summary>
        /// 从模板脚本中创建
        /// </summary>
        /// <param name="uniqueFileNameWithCSExtension">模板位置</param>
        /// <returns>不带拓展的文件名</returns>
        public static void CreateFromTemplate(string uniqueFileNameWithCSExtension)
        {
            string[] guids = AssetDatabase.FindAssets(uniqueFileNameWithCSExtension);
            if (guids.Length == 0)
            {
                Debug.LogWarning($"{uniqueFileNameWithCSExtension}.txt not found in asset database");
                return;
            }
            string templatePath = AssetDatabase.GUIDToAssetPath(guids[0]);

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                ScriptableObject.CreateInstance<CodeGenerator>(),
                uniqueFileNameWithCSExtension,
                EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
                templatePath);

        }

        /// <summary>
        /// 级联生成模板
        /// </summary>
        /// <param name="uniqueFileNameWithCSExtension">根文件名</param>
        /// <param name="casFiles">级联模板文件名列表</param>
        public static void CreateCascadeTemplate(string uniqueFileNameWithCSExtension, List<string> casFiles)
        {
            string[] guids = AssetDatabase.FindAssets(uniqueFileNameWithCSExtension);
            if (guids.Length == 0)
            {
                Debug.LogWarning($"{uniqueFileNameWithCSExtension}.txt not found in asset database");
                return;
            }
            string templatePath = AssetDatabase.GUIDToAssetPath(guids[0]);

            var gen = ScriptableObject.CreateInstance<CascadeGenerator>();
            gen.casasdeList = casFiles;

            ProjectWindowUtil.StartNameEditingIfProjectWindowExists(0,
                gen,
                uniqueFileNameWithCSExtension,
                EditorGUIUtility.IconContent("cs Script Icon").image as Texture2D,
                templatePath);
        }

        #region Generator
        public class CodeGenerator : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                Object file = CreateFromTemplate(pathName, resourceFile);
                ProjectWindowUtil.ShowCreatedAsset(file);
            }
        }

        public class CascadeGenerator : UnityEditor.ProjectWindowCallback.EndNameEditAction
        {
            public List<string> casasdeList = null;

            public override void Action(int instanceId, string pathName, string resourceFile)
            {
                Object file = CreateFromTemplate(pathName, resourceFile);
                ProjectWindowUtil.ShowCreatedAsset(file);
               
                //级联生成
                foreach (var casfile in casasdeList)
                {
                    string[] guids = AssetDatabase.FindAssets(casfile);
                    if (guids.Length == 0)
                    {
                        Debug.LogWarning($"{casfile}.txt not found in asset database");
                        continue;
                    }
                    string templatePath = AssetDatabase.GUIDToAssetPath(guids[0]);

                    string originDir = Path.GetDirectoryName(pathName);
                    string originFileName = Path.GetFileNameWithoutExtension(pathName);
                    var destFileName = casfile.Replace("#", originFileName);

                    Object fileObject = CreateFromTemplate(Path.Combine(originDir, destFileName), templatePath, originFileName);
                    ProjectWindowUtil.ShowCreatedAsset(fileObject);
                }
            }
        }
        #endregion


        /// <summary>
        /// 从模板中创建
        /// </summary>
        /// <param name="destFile">.cs结尾的目标文件路径</param>
        /// <param name="srcFile">.cs.txt结尾的源文件路径</param>
        /// <returns>脚本对象</returns>
        public static Object CreateFromTemplate(string destFile, string srcFile, string parent = "")
        {
            string fileName = Path.GetFileNameWithoutExtension(destFile).Replace(" ", string.Empty);
            UTF8Encoding encoding = new UTF8Encoding(true, false);

            string template = string.Empty;
            using (StreamReader sr = new(srcFile))
            {
                template = sr.ReadToEnd();
            }

            //替换规则
            template = template.Replace("#PARENT#", parent);
            template = template.Replace("#FILENAME#", fileName);
            template = template.Replace("#NOTRIM#", string.Empty);
            template = template.Replace("#NAMESPACE#", ProjectSetting.Raw<MikanLabExtConfig>().templateNamespace);

            using (StreamWriter writer = new(Path.GetFullPath(destFile), false, encoding))
            {
                writer.Write(template);
            }

            AssetDatabase.ImportAsset(destFile);
            return AssetDatabase.LoadAssetAtPath(destFile, typeof(Object));

        }
    }
}