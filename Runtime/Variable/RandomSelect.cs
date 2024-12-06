using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    public interface IRandomSelectable
    {
        public int Weight { get; }
    }
    /// <summary>
    /// 从给定输入中随机抽取
    /// </summary>
    public class RandomSelect<T> where T : IRandomSelectable
    {
        List<T> selection;
        int sum = 0;

        public RandomSelect(List<T> selection)
        {
            this.selection = selection;
            foreach(IRandomSelectable p in selection) sum += p.Weight;
        }
        public T Get()
        {
            int rnum = Random.Range(0, sum);
            int cursum = 0;
            foreach(var p in selection)
            {
                if (cursum < rnum && cursum + p.Weight >= sum) return p;
                cursum += p.Weight;
            }
            return selection[0];
        }
    }
}