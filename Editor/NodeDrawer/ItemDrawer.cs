using UnityEditor.UIElements;

namespace MikanLab
{
    [GraphDrawer(typeof(Item))]
    class ItemDrawer : NodeElementDrawer
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
