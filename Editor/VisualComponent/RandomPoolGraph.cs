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
            
        }
    }
}