using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

namespace MikanLab
{
    public class RandomPoolGraph : NodeGraphElement
    {
        public RootNode root;
        
        public RandomPoolGraph(RandomPool target) : base(target) { }

        
        public override void Execute()
        {
            var rootEdge = root.OutputPort.connections.FirstOrDefault();
            if (rootEdge == null) return;

            var currentNode = rootEdge.input.node as GraphNode;

            while (true)
            {
                currentNode.Execute();

                if(currentNode.OutputPort == null) break;
                var edge = currentNode.OutputPort.connections.FirstOrDefault();
                if (edge == null) break;

                currentNode = edge.input.node as GraphNode;
            }
        }
    }
}