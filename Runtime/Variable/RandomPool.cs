using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 概率池
/// </summary>
public class ProbabilityPool : ScriptableObject
{

    /// <summary>
    /// 结果池
    /// </summary>
    List<int> pool;

    /// <summary>
    /// 池中物体类型
    /// </summary>
    class Item
    {
        public int weight;
        public string item;
    }

    int sum = 0;

    /// <summary>
    /// 运行时增加项
    /// </summary>
    /// <param name="weight">权重</param>
    /// <param name="item">物品ID</param>
    public void Add(int weight, int item)
    {

    }

    /// <summary>
    /// 运行时修改项
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="item"></param>
    public void Edit(int weight, int item)
    {

    }

    /// <summary>
    /// 运行时删除项
    /// </summary>
    /// <param name="weight"></param>
    /// <param name="item"></param>
    public void Remove(int weight, int item)
    {

    }
}
