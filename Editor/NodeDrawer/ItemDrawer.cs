using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

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
            
            visualNode.extensionContainer.Add(text);
        }
    }
}
