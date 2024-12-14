using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;

namespace MikanLab
{
    public class RandomPoolWindow : EditorWindow
    {
        private static readonly string prefKey = "MikanLab_RandomPoolWindow";
        private bool isOpenPropertyWindow = false;
        private bool ifInited = false;
        private bool ifFirst = true;
        private RandomPool target;
        private Setting setting;
        private Toolbar toolbar;

        #region 偏好设置
        [Serializable]
        public class Setting
        {
            public float width = 500f;
            public float height = 300f;
            public float position_x = 0f;
            public float position_y = 0f;
            public bool propertyWindowOn = false;
        }
        #endregion

        #region 生命周期
        public static void Invoke(RandomPool target)
        {
            var window = GetWindow<RandomPoolWindow>("RandomPoolWindow");
            if (!window.ifInited)
            {
                window.target = target;
                if (!EditorPrefs.HasKey(prefKey)) window.setting = new();
                else window.setting = JsonUtility.FromJson<Setting>(EditorPrefs.GetString(prefKey));
                window.FromPref();
                window.AddElements();
                window.ifInited = true;
                window.ifFirst = false;
            }
        }

        private void OnEnable()
        {
            if (!ifInited && !ifFirst)
            {
                if (!EditorPrefs.HasKey(prefKey)) setting = new();
                else setting = JsonUtility.FromJson<Setting>(EditorPrefs.GetString(prefKey));
                FromPref();
                AddElements();
                ifInited = true;
                ifFirst = false;
            }
        }

        private void OnDestroy()
        {
            SavePref();
            SaveGraph();
        }

        private void OnDisable()
        {
            ifInited = false;
            SavePref();
            SaveGraph();
        }
        #endregion

        #region 元素组件
        private RandomPoolGraph graph;
        private NodeGraphElement Graph
        {
            get
            {
                if (graph == null)
                {
                    graph = new RandomPoolGraph(target) { style = { flexGrow = 1 } };
                    graph.LoadFromAsset();
                }
                return graph;
            }

        }

        private PropertyWindow propertyWindow;
        private PropertyWindow PropertyWindow
        {
            get
            {
                if (propertyWindow == null)
                {
                    propertyWindow = new PropertyWindow(isOpenPropertyWindow);
                }
                return propertyWindow;
            }
        }

        #endregion

        #region 绘制控制
        private void AddElements()
        {
            toolbar = new();
            toolbar.style.flexDirection = FlexDirection.Row;
            rootVisualElement.Add(toolbar);

            var propertyWindow = PropertyWindow;
            

            //工具栏
            var pro = new ToolbarToggle() { text = "属性" };
            pro.SetValueWithoutNotify(isOpenPropertyWindow);
            pro.RegisterValueChangedCallback(ctx => PropertyWindow.Reverse());
            toolbar.Add(pro);
            toolbar.Add(new ToolbarButton(Graph.Execute) { text = "测试" });

            Graph.Add(propertyWindow);
            rootVisualElement.Add(Graph);
            

        }

        private void FromPref()
        {
            position = new(setting.position_x, setting.position_y, setting.width, setting.height);
            isOpenPropertyWindow = setting.propertyWindowOn;
        }
        #endregion

        #region 保存
        private void SaveGraph()
        {
            if (target == null) return;
            if (graph == null) return;

            graph.SaveChangeToAsset();
            EditorUtility.SetDirty(target);
            AssetDatabase.SaveAssets();
            AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(target));
        }

        private void SavePref()
        {
            setting.width = position.width;
            setting.height = position.height;
            setting.position_x = position.x;
            setting.position_y = position.y;
            setting.propertyWindowOn = PropertyWindow.isOpen;
            EditorPrefs.SetString(prefKey, JsonUtility.ToJson(setting));
        }
        #endregion
    }
}