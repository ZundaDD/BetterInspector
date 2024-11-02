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
    public class LifeCycle : MonoBehaviour
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
        /// 线性时钟事件
        /// </summary>
        private class ClockEvent
        {
            /// <summary>
            /// 如果不是队列中的第一个事件，则表示在前驱事件执行完之后，等待多久执行该事件
            /// 否则表示离当前事件执行还剩下多少时间
            /// </summary>
            public float leftTime;

            /// <summary>
            /// 当前执行的函数，注意，如果使用匿名委托，在调用CancelEvent的时候将无法判断相等
            /// </summary>
            public Action eventAction;

            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="leftTime">剩余事件</param>
            /// <param name="eventAction">执行事件</param>
            public ClockEvent(float leftTime, Action eventAction)
            {
                this.leftTime = leftTime;
                this.eventAction = eventAction;
            }
        }

        /// <summary>
        /// 存储线性时钟事件的链表
        /// </summary>
        private static LinkedList<ClockEvent> clockEvents = new();

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
        public void Register(ILifeCycle registration) => RegisterList.Add(registration);

        /// <summary>
        /// 手动解除注册
        /// </summary>
        /// <param name="registration"></param>
        public void Unregister(ILifeCycle registration) => UnregisterList.Remove(registration);

        /// <summary>
        /// 更新
        /// </summary>
        void Update()
        {
            Tick();

            foreach (var item in Watch) { item.Update(); }
        }

        /// <summary>
        /// 线性钟步进
        /// </summary>
        private void Tick()
        {
            float leftDeltaTime = Time.deltaTime;

            //依次对队首进行判断，直到deltaTime被耗完或者队列中没有事件
            while (leftDeltaTime > 0 && clockEvents.Count != 0)
            {
                var soloNode = clockEvents.First;

                //如果队首事件剩余时间大于deltaTime，则只减少
                if (soloNode.Value.leftTime > leftDeltaTime)
                {
                    soloNode.Value.leftTime -= leftDeltaTime;
                    leftDeltaTime = 0;
                }
                //如果不是，则执行并退出队首,减少相应的deltaTime
                else
                {
                    leftDeltaTime -= soloNode.Value.leftTime;
                    soloNode.Value.eventAction.Invoke();

                    clockEvents.RemoveFirst();
                    soloNode = clockEvents.First;
                }
            }
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

            //更新托管数目
            watchCount = Watch.Count;

            //处理更新
            foreach (var item in Watch) { item?.FixedUpdate(); }

            //处理注册
            foreach (var item in RegisterList) Watch.Add(item);
            RegisterList.Clear();

        }

        /// <summary>
        /// 插入一个延迟执行的事件
        /// </summary>
        /// <param name="action">延迟执行的事件</param>
        /// <param name="delaySeconds">延迟的时间</param>
        /// <returns>插入是否成功</returns>
        public static bool Delay(float delaySeconds, System.Action action)
        {
            //排除不成功情况
            if (delaySeconds <= 0) return false;

            //判断是否队列为空
            //如果为空则直接插入
            if (clockEvents.Count == 0) clockEvents.AddFirst(new ClockEvent(delaySeconds, action));
            //如果不为空
            else
            {
                //寻找插入位置
                LinkedListNode<ClockEvent> soloNode = clockEvents.First;
                float sum = 0;

                while (soloNode != null)
                {
                    sum += soloNode.Value.leftTime;

                    //如果当前累计和小于延迟时间，则说明插入位置在后方
                    if (sum < delaySeconds) soloNode = soloNode.Next;

                    //如果当前累计和大于延迟时间，则说明插入位置就在这个事件前
                    if (sum > delaySeconds)
                    {
                        clockEvents.AddBefore(soloNode, new ClockEvent(delaySeconds + soloNode.Value.leftTime - sum, action));

                        //修改当前结点的值
                        soloNode.Value.leftTime = sum - delaySeconds;

                        //无需进行其他调整，直接返回成功
                        return true;
                    }

                    //如果相等，则直接加在委托中
                    if (sum == delaySeconds)
                    {
                        soloNode.Value.eventAction += action;

                        //无需进行其他调整，直接返回成功
                        return true;
                    }
                }

                //如果遍历都没有匹配成功，则一定加在最末尾
                if (soloNode == null)
                {
                    clockEvents.AddLast(new ClockEvent(delaySeconds - sum, action));
                }

            }

            return true;
        }
    }
}