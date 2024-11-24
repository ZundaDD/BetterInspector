using UnityEngine.UIElements;

namespace MikanLab
{
    public class PropertyWindow : VisualElement
    {
        public bool isOpen = false;
        public void Reverse()
        {
            isOpen = !isOpen;
            style.display = isOpen ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}