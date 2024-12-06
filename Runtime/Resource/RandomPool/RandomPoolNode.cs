using System;
using System.Collections.Generic;
using UnityEngine;

namespace MikanLab
{
    public abstract class RandomPoolBaseNode : BaseNode
    {
        public abstract List<int> Execute(bool[] visit);
    }

    /// <summary>
    /// 输入节点，用于接收参数来进行遍历
    /// </summary>
    [Serializable]
    [UsedFor(typeof(RandomPool))]
    public class Input : RandomPoolBaseNode
    {
        public Input() 
        {
            NodeName = "输入";
            AddOutputPort(typeof(int), "Link", true);
        }

        public override List<int> Execute(bool[] visit)
        {
            List<int> result = new List<int>();

            List<Weight> weightNode = new();
            foreach(var node in OutputPorts["Link"].Edges)
            {
                if (visit[node.TargetIndex]) continue;

                visit[node.TargetIndex] = true;
                if (Owner.NodeList[node.TargetIndex].GetType() == typeof(Weight))
                {
                    weightNode.Add(Owner.NodeList[node.TargetIndex] as Weight);
                }
                else result.AddRange((Owner.NodeList[node.TargetIndex] as RandomPoolBaseNode).Execute(visit));
  
            }

            if (weightNode.Count > 0)
            {
                RandomSelect<Weight> roll = new(weightNode);
                result.AddRange(roll.Get().Execute(visit));
            }

            return result;
        }
    }

    /// <summary>
    /// 输出节点，用于接收结果并输出给GetResult
    /// </summary>
    [Serializable]
    [UsedFor(typeof(RandomPool))]
    public class Output : RandomPoolBaseNode
    {
        public Output()
        {
            NodeName = "输出";
            AddInputPort(typeof(int), "Result", true);
        }

        public override List<int> Execute(bool[] visit)
        {
            visit[index] = false;
            return new();
        }
    }

    public abstract class InOutNode : RandomPoolBaseNode
    {
        public InOutNode()
        {
            AddInputPort(typeof(int), "In");
            AddOutputPort(typeof(int), "Out");
        }

        public override List<int> Execute(bool[] visit)
        {
            List<int> result = new();
            //Add Typical Node's Operation Here
            

            List<Weight> weightNode = new();
            foreach (var node in OutputPorts["Out"].Edges)
            {
                if (visit[node.TargetIndex]) continue;

                visit[node.TargetIndex] = true;
                
                if (Owner.NodeList[node.TargetIndex].GetType() == typeof(Weight))
                {
                    weightNode.Add(Owner.NodeList[node.TargetIndex] as Weight);
                }
                else result.AddRange((Owner.NodeList[node.TargetIndex] as RandomPoolBaseNode).Execute(visit));


            }

            if (weightNode.Count > 0)
            {
                RandomSelect<Weight> roll = new(weightNode);
                result.AddRange(roll.Get().Execute(visit));
            }

            return result;
        }
    }

    /// <summary>
    /// 权重节点，用于控制分清支路
    /// </summary>
    [Serializable]
    [UsedFor(typeof(RandomPool))]
    public class Weight : InOutNode,IRandomSelectable
    {
        public int weight;
        int IRandomSelectable.Weight => weight;
        
        public Weight()
        {
            NodeName = "权重";
        }

    }

    [UsedFor(typeof(RandomPool))]
    public class Condition: InOutNode
    {
        delegate bool Predicate();

        Predicate predicate;
        public Condition()
        {
            NodeName = "条件";
        }

        public override List<int> Execute(bool[] visit)
        {
            if(!predicate()) return new();

            return base.Execute(visit);
        }
    }

    [UsedFor(typeof(RandomPool))]
    public class Item : InOutNode
    {
        public int item;
        public int count = 1;

        public Item()
        {
            NodeName = "待选项";
        }

        public override List<int> Execute(bool[] visit)
        {
            List<int> result = new List<int>();

            for(int i = 0;i < count;++i) result.Add(item);

            result.AddRange(base.Execute(visit));

            return result;
        }
    }

    [UsedFor(typeof(RandomPool))]
    public class Count : InOutNode
    {
        public int min;
        public int max;

        public Count()
        {
            NodeName = "数量";
        }
    }
}
