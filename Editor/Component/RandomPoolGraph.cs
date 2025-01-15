using UnityEngine;

namespace MikanLab
{
    using NodeGraph;
    [CustomGraphView(typeof(RandomPool))]
    public class RandomPoolGraph : NodeGraphView
    {
        public RandomPoolGraph() : base()
        {
            AddToClassList("Mikan-graph-view");
            styleSheets.Add(EditorResources.GraphViewColored);
        }

        public override void Execute()
        {
            foreach(int i in (target as RandomPool).GetResult(new Parameter[0]))
            {
                Debug.Log(i);
            }
        }
    }
}