using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 线性时钟事件
    /// </summary>
    public class ClockEvent : ILifeCycle
    {
        /// <summary>
        /// 剩余时间
        /// </summary>
        private float leftTime;

        /// <summary>
        /// 当前执行的函数，注意，如果使用匿名委托，在调用CancelEvent的时候将无法判断相等
        /// </summary>
        private Action eventAction;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="leftTime">剩余事件</param>
        /// <param name="eventAction">执行事件</param>
        public ClockEvent(float leftTime, Action eventAction)
        {
            this.leftTime = leftTime;
            this.eventAction = eventAction;
            LifeCycle.Register(this);
        }

        public void FixedUpdate() { }

        public bool IfDelete() => leftTime < 0;

        public void Update()
        {
            leftTime -= Time.deltaTime;
            if (leftTime < 0) eventAction.Invoke();
        }
    }
}