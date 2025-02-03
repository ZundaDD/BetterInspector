using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

namespace MikanLab
{
    /// <summary>
    /// 任务进度条
    /// </summary>
    [Serializable]
    public class TaskProgress
    {
        #region 子对象
        /// <summary>
        /// 任务对象
        /// </summary>
        public class Task
        {
            public int Weight;
            public string Description;
            public Func<UniTask> Func;
        }
        #endregion

        #region 公开数据接口
        /// <summary>
        /// 进度浮点数，介于0-1之间
        /// </summary>
        public float ProgressFloat => (float)currentWeight / totalWeight;

        /// <summary>
        /// 当前任务描述，用于显示
        /// </summary>
        public string CurrentTaskDescription => currentText;

        /// <summary>
        /// 剩余任务数
        /// </summary>
        public int LeftTask => taskList.Count == 0 ? 0 : taskList.Count + 1;

        /// <summary>
        /// 已经完成的任务
        /// </summary>
        public int HandledTask => allTask - taskList.Count;

        /// <summary>
        /// 全部任务数
        /// </summary>
        public int Alltask => allTask;

        /// <summary>
        /// 完成委托
        /// </summary>
        public Action OnFinished;
        #endregion

        private Queue<Task> taskList = new();
        private int allTask = 0;
        private string finishText = "";
        private string currentText = "";
        private int totalWeight = 0;
        private int currentWeight = 0;
        private bool isRunning = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="finishText"></param>
        public TaskProgress(string finishText = "Tasks all finished...")
        {
            this.finishText = finishText;
        }

        /// <summary>
        /// 向队列中添加任务
        /// </summary>
        /// <param name="func">同步任务体</param>
        /// <param name="description">任务描述</param>
        /// <param name="weight">任务权重</param>
        public void AddTask(Action func, string description = "", int weight = 1)
        {
            AddTask(() => UniTask.RunOnThreadPool(func), description, weight);
        }

        /// <summary>
        /// 向队列中添加任务
        /// </summary>
        /// <param name="func">UniTask任务体</param>
        /// <param name="description">任务描述</param>
        /// <param name="weight">任务权重</param>
        public void AddTask(Func<UniTask> func, string description = "", int weight = 1)
        {
            taskList.Enqueue(new() { Description = description, Func = func, Weight = weight });
            totalWeight += weight;
            allTask++;
        }

        /// <summary>
        /// 启动进度条
        /// </summary>
        /// <exception cref="Exception">空任务</exception>
        public void Start()
        {
            if (taskList.Count == 0)
            {
                Debug.LogError("任务列表为空！");
                return;
            }
            if (isRunning)
            {
                Debug.LogError("任务已经开始执行！无法重复开始");
                return;
            }

            currentWeight = 0;
            isRunning = true;
            RunTasks().Forget();
        }


        private async UniTask RunTasks()
        {
            try
            {
                while (taskList.Count > 0)
                {
                    var task = taskList.Dequeue();
                    currentText = task.Description;
                    await task.Func();
                    await UniTask.Yield();

                    Debug.Log($"Task {task.Description} Finished!");
                    currentWeight += task.Weight;
                }

                currentText = finishText;
                OnFinished?.Invoke();
                OnFinished = null;
                isRunning = false;
            }
            catch (OperationCanceledException)
            {
                Debug.Log("任务被取消");
            }
            catch (ThreadAbortException)
            {
                Debug.Log("任务被强制终止");
            }
            catch (Exception ex)
            {
                Debug.LogError("任务发生异常：" + ex.Message);
            }
        }
    }
}