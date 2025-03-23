using MikanLab;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    public class ProcessOpen : MonoBehaviour
    {
        [OnOpenAsset(-1)]
        public static bool ProcessAssetOpen(int instanceID, int line)
        {

            var obj = EditorUtility.InstanceIDToObject(instanceID);
            string name = AssetDatabase.GetAssetPath(obj);

            //依次处理
            if (obj is ConfigFile)
            {
                var window = EditorWindow.GetWindow<ConfigFileWindow>("配置文件编辑器");
                window.BindFile(obj as ConfigFile);
                return true;
            }





            return false;
        }
    }
}