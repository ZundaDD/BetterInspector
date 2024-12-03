


namespace MikanLab
{
    /// <summary>
    /// 输入节点，用于接收参数来进行遍历
    /// </summary>
    [UsedFor(typeof(RandomPool))]
    class Input : BaseNode
    {
        public Input() 
        {
            NodeName = "输入";
            AddOutputPort(typeof(int), "Link", true);
        }
    }

    /// <summary>
    /// 输出节点，用于接收结果并输出给GetResult
    /// </summary>
    [UsedFor(typeof(RandomPool))]
    class Output : BaseNode
    {
        public Output()
        {
            NodeName = "输出";
            AddInputPort(typeof(int), "Result", true);
        }
    }

    abstract class InOutNode : BaseNode 
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
    [UsedFor(typeof(RandomPool))]
    class Weight : InOutNode
    {
        public int weight;

        public Weight()
        {
            NodeName = "权重";
        }
    }

    [UsedFor(typeof(RandomPool))]
    class Condition: InOutNode
    {
        public Condition()
        {
            NodeName = "条件";
        }
    }

    [UsedFor(typeof(RandomPool))]
    class Item : InOutNode
    {
        public int item;
        public int count;

        public Item()
        {
            NodeName = "待选项";
        }
    }

    [UsedFor(typeof(RandomPool))]
    class Count : InOutNode
    {
        public int min;
        public int max;

        public Count()
        {
            NodeName = "数量";
        }
    }
}
