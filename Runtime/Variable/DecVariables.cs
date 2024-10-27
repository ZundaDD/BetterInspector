using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    [Serializable]
    /// <summary>
    /// 按帧自减的无符号整形
    /// </summary>
    public class DecUInt : ILifeCycle
    {

        
        /// <summary>
        /// 计时器
        /// </summary>
        [SerializeField] private uint frameCount = 0;

        /// <summary>
        /// 物体引用
        /// </summary>
        private GameObject owner = null;

        public DecUInt(GameObject owner)
        {
            this.owner = owner;
        }

        public void FixedUpdate() { if (frameCount > 0) frameCount--; }

        public bool Zero() => frameCount == 0;

        public void Set(uint value)
        {
            if (frameCount == 0) LifeCycle.Instance.Register(this);
            frameCount = value;
        }

        public void Set(float value)
        {
            if(value < 0) throw new ArgumentOutOfRangeException($"{value} is negative");
            Set((uint)(value / Time.fixedDeltaTime));
        }

        public uint Value => frameCount;

        public bool IfDelete() => owner == null || frameCount == 0;

        public void Update() { }
    }

    [Serializable]
    /// <summary>
    /// 按帧自减，在归零时调用绑定委托
    /// </summary>
    public class DelegateUInt : ILifeCycle
    {
        /// <summary>
        /// 计时器
        /// </summary>
        [SerializeField] private uint frameCount = 0;

        /// <summary>
        /// 委托
        /// </summary>
        public System.Action doWhat;

        /// <summary>
        /// 物体引用
        /// </summary>
        private GameObject owner;

        
        public DelegateUInt(GameObject owner, Action doWhat)
        {
            this.owner = owner;
            this.doWhat = doWhat;
        }

        public uint Value => frameCount;

        //按帧更新
        public void FixedUpdate()
        {
            frameCount--;
            if (frameCount == 0) doWhat?.Invoke();
        }

        public void Set(uint value)
        {
            if (frameCount != 0) return;

            LifeCycle.Instance.Register(this);
            frameCount = value;
        }

        public void Set(float value)
        {
            if (value < 0) throw new ArgumentOutOfRangeException($"{value} is negative");
            Set((uint)(value / Time.fixedDeltaTime));
        }

        public bool IfDelete() { return frameCount == 0 || owner == null; }

        /// <summary>
        /// 提前触发
        /// </summary>
        public void Trigger()
        {
            if (frameCount == 0) return;

            LifeCycle.Instance.Unregister(this);
            doWhat?.Invoke();
            frameCount = 0;
        }

        /// <summary>
        /// 取消触发
        /// </summary>
        public void Cancel()
        {
            if(frameCount == 0) return;

            LifeCycle.Instance.Unregister(this);
            frameCount = 0;
        }

        /// <summary>
        /// 延长触发时间
        /// </summary>
        /// <param name="length">延长时长</param>
        public void Lengthen(uint length)
        {
            if (frameCount == 0) return;

            frameCount += length;
        }

        /// <summary>
        /// 延长触发时间
        /// </summary>
        /// <param name="length">延长时长</param>
        public void Lengthen(float length)
        {
            if (length < 0) throw new ArgumentOutOfRangeException($"{length} is negative");
            Lengthen((uint)(length / Time.fixedDeltaTime));
        }

        public void Update() { }
    }
}