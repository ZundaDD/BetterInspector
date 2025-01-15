using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor;
using UnityEngine;

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
    #endregion


    Queue<Task> taskList = new();
    string finishText = "";
    string currentText = "";
    int totalWeight = 0;
    int currentWeight = 0;
    bool isRunning = false;

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
        AddTask(() => UniTask.RunOnThreadPool(func) , description, weight);
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
    }

    /// <summary>
    /// 启动进度条
    /// </summary>
    /// <exception cref="Exception">空任务</exception>
    public void Start()
    {
        if (taskList.Count == 0)
        {
            throw new Exception("任务列表为空！");
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
