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
        
        public RandomPoolGraph(RandomPool target) : base(target) { }

        
        public override void Execute()
        {
            foreach(int i in (target as RandomPool).GetResult(new Parameter[0]))
            {
                Debug.Log(i);
            }
        }
    }
}