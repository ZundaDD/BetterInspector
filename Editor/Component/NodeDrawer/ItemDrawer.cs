using UnityEditor.UIElements;


namespace MikanLab
{
    using NodeGraph;

    [CustomNodeDrawer(typeof(Item))]
    class ItemDrawer : NodeDrawer
    {
        public override void OnDrawer()
        {
            base.OnDrawer();

            var itemproperty = property.FindPropertyRelative("item");
            PropertyField text = new();
            text.BindProperty(itemproperty);
            text.styleSheets.Add(GUIUtilities.PropertyFieldLessenLabel);

            var countproperty = property.FindPropertyRelative("count");
            PropertyField count = new();
            count.BindProperty(countproperty);
            count.styleSheets.Add(GUIUtilities.PropertyFieldLessenLabel);

            visualNode.extensionContainer.Add(text);
            visualNode.extensionContainer.Add(count);
        }
    }
}
