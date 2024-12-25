using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System;
using UnityEditor.UIElements;

namespace MikanLab
{
    using NodeGraph;
    [CustomGraphWindow(typeof(RandomPool))]
    public class RandomPoolWindow : NodeGraphWindow
    {
        protected override string prefKey => "MikanLab_Random_Pool_Window";
        protected override Type prefType => typeof(RandomPoolWindow.Setting);

        private bool isOpenPropertyWindow = false;

        #region 偏好设置
        [Serializable]
        public new class Setting : NodeGraphWindow.Setting
        {
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
                window.SetLayout(window.prefKey, window.prefType);
            }
        }

        #endregion

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


        #region 绘制控制
        protected override void AddElements()
        {
            base.AddElements();

            var propertyWindow = PropertyWindow;
            
            //工具栏
            var pro = new ToolbarToggle() { text = "属性" };
            pro.SetValueWithoutNotify(isOpenPropertyWindow);
            pro.RegisterValueChangedCallback(ctx => PropertyWindow.Reverse());
            toolbar.Add(pro);

            graph.Add(propertyWindow);

        }

        protected override void SetFromPref()
        {
            base.SetFromPref();
            isOpenPropertyWindow = (setting as Setting).propertyWindowOn;
        }
        #endregion

        #region 保存
        protected override void SavePref(string keyname)
        {
            (setting as Setting).propertyWindowOn = PropertyWindow.isOpen;
            base.SavePref(keyname);
        }
        #endregion
    }
}