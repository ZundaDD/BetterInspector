using System;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 需要进行生命托管的对象接口
    /// </summary>
    public interface ILifeCycle
    {
        /// <summary>
        /// 绘画帧更新
        /// </summary>
        void Update();
        
        /// <summary>
        /// 物理帧更新
        /// </summary>
        void FixedUpdate();

        /// <summary>
        /// 托管结束条件
        /// </summary>
        /// <returns>是否结束</returns>
        bool IfDelete();
    }

    //LifeCycle用于对托管对象的生命循环做出集中处理
    //实现Unity Monobehaviour组件基本功能：初始化，时钟,(预计加入销毁)
    //托管对象不需要作为脚本挂在物体上
    public partial class LifeCycle : MonoBehaviour
    {
        //静态成员指向当前的单例对象
        private static LifeCycle instance;

        public static LifeCycle Instance
        {
            get
            {
                if (instance == null)
                {
                    var gameobject = new GameObject("LifeCycle");
                    DontDestroyOnLoad(gameobject);
                    instance = gameobject.AddComponent<LifeCycle>();
                }
                return instance;
            }
        }

        /// <summary>
        /// 当前托管列表
        /// </summary>
        private static HashSet<ILifeCycle> Watch = new();

        /// <summary>
        /// 注册队列
        /// </summary>
        private static List<ILifeCycle> RegisterList = new();

        /// <summary>
        /// 解绑队列
        /// </summary>
        private static List<ILifeCycle> UnregisterList = new();

        /// <summary>
        /// 托管变量数目
        /// </summary>
        public int watchCount;

        /// <summary>
        /// 注册以开始托管
        /// </summary>
        /// <param name="registration"></param>
        public static void Register(ILifeCycle registration)
        {
            Instance.watchCount++;
            RegisterList.Add(registration);
        }
        /// <summary>
        /// 手动解除注册
        /// </summary>
        /// <param name="registration"></param>
        public static void Unregister(ILifeCycle registration)
        {
            Instance.watchCount--;
            UnregisterList.Add(registration);
        }

        /// <summary>
        /// 更新
        /// </summary>
        void Update()
        {
            foreach (var item in Watch) { item.Update(); }
        }

        /// <summary>
        /// 帧更新
        /// </summary>
        void FixedUpdate()
        {
            //按条件移除不再托管的对象
            Watch.RemoveWhere(x => x.IfDelete());

            //处理解绑
            foreach (var item in UnregisterList) Watch.Remove(item);
            UnregisterList.Clear();

            //处理更新
            foreach (var item in Watch) { item?.FixedUpdate(); }

            //处理注册
            foreach (var item in RegisterList) Watch.Add(item);
            RegisterList.Clear();

        }
    }
}