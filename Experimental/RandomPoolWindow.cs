using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class RandomPoolWindow : EditorWindow
{
    private bool isOpenPropertyWindow = false;
    private RandomPool target;

    
    

    #region 生命周期
    public static void Invoke(RandomPool target)
    {
        var window = GetWindow<RandomPoolWindow>("RandomPoolWindow");
        window.target = target;
        window.AddElements();
    }
    private void OnDestroy()
    {
        Save();
    }

    private void OnDisable()
    {
        Save();
    }
    #endregion

    #region 元素组件
    private RandomPoolGraph graph;
    private VisualElement Graph
    {
        get
        {
            if (graph == null)
            {
                graph = new RandomPoolGraph(target) { style = { flexGrow = 1 } };
                graph.LoadFromAsset();
            }
            return graph;
        }
        
    }

    private PropertyWindow propertyWindow;
    private PropertyWindow PropertyWindow
    {
        get
        {
            if(propertyWindow == null)
            {
                propertyWindow = new PropertyWindow();
            }
            return propertyWindow;
        }
    }

    #endregion
    

    private void AddElements()
    {
        var propertyWindow = PropertyWindow;
        rootVisualElement.Add(new Button(propertyWindow.Reverse) { text = "属性" });

        rootVisualElement.Add(PropertyWindow);
        rootVisualElement.Add(Graph);
        //rootVisualElement.Add(new Button(graphView.Execute) { text = "Execute" });
    }

    private void Save()
    {
        if(target == null) return;
        if (graph == null) return;

        graph.SaveChangeToAsset();
        EditorUtility.SetDirty(target);
        AssetDatabase.SaveAssets();
        AssetDatabase.ImportAsset(AssetDatabase.GetAssetPath(target));
    }
}
