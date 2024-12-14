using UnityEngine.UIElements;
using UnityEngine;

namespace MikanLab
{
    public class PropertyWindow : Box
    {
        protected ScrollView scrollView;
        public bool isOpen = false;
        public void Reverse()
        {
            isOpen = !isOpen;
            style.display = isOpen ? DisplayStyle.Flex : DisplayStyle.None;
        }

        public PropertyWindow(bool initState)
        {
            scrollView = new ScrollView();
            Add(scrollView);

            scrollView.Add(new Label("Name:"));
            scrollView.Add(new TextField());
            scrollView.Add(new Label("Name:"));
            scrollView.Add(new TextField());
            scrollView.Add(new Label("Name:"));
            scrollView.Add(new TextField());
            scrollView.Add(new Label("Name:"));
            scrollView.Add(new TextField());
            scrollView.Add(new Label("Name:"));
            scrollView.Add(new TextField());
            scrollView.Add(new Label("Name:"));
            scrollView.Add(new TextField());
            scrollView.Add(new Label("Name:"));
            scrollView.Add(new TextField());

            styleSheets.Add(GUIUtilities.PropertyBox);
            AddToClassList("Mikan-Property-Box");

            isOpen = initState;
            style.display = initState ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}