using UnityEditor.UIElements;

namespace MikanLab
{
    [GraphDrawer(typeof(Weight))]
    class WeightDrawer : NodeElementDrawer
    {
        public override void OnDrawer()
        {
            base.OnDrawer();

            var weightproperty = property.FindPropertyRelative("weight");
            PropertyField weight = new();
            weight.BindProperty(weightproperty);
            weight.styleSheets.Add(GUIUtilities.PropertyFieldLessenLabel);

            visualNode.extensionContainer.Add(weight);
        }
    }
}
