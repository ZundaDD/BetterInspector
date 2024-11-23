using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PropertyWindow : VisualElement
{
    public bool isOpen = false;
    public void Reverse()
    {
        isOpen = !isOpen;
        style.display = isOpen ? DisplayStyle.Flex : DisplayStyle.None;
    }
}
