using System;
using System.Collections.Generic;
using UnityEngine.Events;

namespace MikanLab
{
    /// <summary>
    /// 输入节点，用于接收参数来进行遍历
    /// </summary>
    [Serializable]
    [UsedFor(typeof(RandomPool))]
    public class Input : BaseNode
    {
        public Input() 
        {
            NodeName = "输入";
            AddOutputPort(typeof(int), "Link", true);
        }

        public override List<int> Execute(bool[] visit, List<int> result)
        {
            List<Weight> weightNode = new();
            foreach(var node in OutputPorts["Link"].Edges)
            {
                if (visit[node.TargetIndex]) continue;

                visit[node.TargetIndex] = true;
                if (Owner.NodeList[node.TargetIndex].GetType() == typeof(Weight))
                {
                    weightNode.Add(Owner.NodeList[node.TargetIndex] as Weight);
                }
                else result.AddRange(Owner.NodeList[node.TargetIndex].Execute(visit, result));

                
            }

            RandomSelect<Weight> roll = new(weightNode);
            result.AddRange(roll.Get().Execute(visit, result));

            return result;
        }
    }

    /// <summary>
    /// 输出节点，用于接收结果并输出给GetResult
    /// </summary>
    [Serializable]
    [UsedFor(typeof(RandomPool))]
    public class Output : BaseNode
    {
        public Output()
        {
            NodeName = "输出";
            AddInputPort(typeof(int), "Result", true);
        }

        public override List<int> Execute(bool[] visit, List<int> result)
        {
            visit[index] = false;
            return result;
        }
    }

    public abstract class InOutNode : BaseNode 
    {
        public InOutNode()
        {
            AddInputPort(typeof(int), "In");
            AddOutputPort(typeof(int), "Out");
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

        public override List<int> Execute(bool[] visit, List<int> result)
        {
            List<Weight> weightNode = new();
            foreach (var node in OutputPorts["Link"].Edges)
            {
                if (visit[node.TargetIndex]) continue;

                visit[node.TargetIndex] = true;
                if (Owner.NodeList[node.TargetIndex].GetType() == typeof(Weight))
                {
                    weightNode.Add(Owner.NodeList[node.TargetIndex] as Weight);
                }
                else result.AddRange(Owner.NodeList[node.TargetIndex].Execute(visit, result));


            }

            RandomSelect<Weight> roll = new(weightNode);
            result.AddRange(roll.Get().Execute(visit, result));

            return result;
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

        public override List<int> Execute(bool[] visit, List<int> result)
        {
            if(!predicate()) return result;

            List<Weight> weightNode = new();
            foreach (var node in OutputPorts["Link"].Edges)
            {
                if (visit[node.TargetIndex]) continue;

                visit[node.TargetIndex] = true;
                if (Owner.NodeList[node.TargetIndex].GetType() == typeof(Weight))
                {
                    weightNode.Add(Owner.NodeList[node.TargetIndex] as Weight);
                }
                else result.AddRange(Owner.NodeList[node.TargetIndex].Execute(visit, result));


            }

            RandomSelect<Weight> roll = new(weightNode);
            result.AddRange(roll.Get().Execute(visit, result));

            return result;
        }
    }

    [UsedFor(typeof(RandomPool))]
    public class Item : InOutNode
    {
        public int item;
        public int count;

        public Item()
        {
            NodeName = "待选项";
        }

        public override List<int> Execute(bool[] visit, List<int> result)
        {
            for(int i = 0;i < count;++i) result.Add(item);

            List<Weight> weightNode = new();
            foreach (var node in OutputPorts["Link"].Edges)
            {
                if (visit[node.TargetIndex]) continue;

                visit[node.TargetIndex] = true;
                if (Owner.NodeList[node.TargetIndex].GetType() == typeof(Weight))
                {
                    weightNode.Add(Owner.NodeList[node.TargetIndex] as Weight);
                }
                else result.AddRange(Owner.NodeList[node.TargetIndex].Execute(visit, result));


            }

            RandomSelect<Weight> roll = new(weightNode);
            result.AddRange(roll.Get().Execute(visit, result));

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
