using UnityEditor.UIElements;

namespace MikanLab
{
    using NodeGraph;

    [CustomNodeDrawer(typeof(Weight))]
    class WeightDrawer : NodeDrawer
    {
        public override void OnDrawer()
        {
            base.OnDrawer();

            var weightproperty = property.FindPropertyRelative("weight");
            PropertyField weight = new();
            weight.BindProperty(weightproperty);
            weight.styleSheets.Add(EditorResources.PropertyFieldLessenLabel);

            visualNode.extensionContainer.Add(weight);
        }
    }
}
