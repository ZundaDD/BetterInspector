using System;

namespace MikanLab
{
    [Serializable]
    public abstract class GraphNode : BaseNode
    {

        public GraphNode()
        {
            AddInputPort(typeof(string), "In");
            AddOutputPort(typeof(string), "Out");
        }

    }

    [Serializable]
    public class LogNode : GraphNode
    {
        public LogNode()
        {
            NodeName = "Log";

            AddInputPort(typeof(string));
        }
    }

    [Serializable]
    public class StringNode : BaseNode
    {
        
        public string Ts;
        

        public StringNode()
        {
            NodeName = "String";

            AddOutputPort(typeof(string));
        }

    }

    [Serializable]
    public class RootNode : BaseNode
    {

        public RootNode()
        {
            Deleteable = false;
            NodeName = "Root";
            AddOutputPort(typeof(string), "Out");
        }
    }
}