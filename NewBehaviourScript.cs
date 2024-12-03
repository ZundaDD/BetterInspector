using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MikanLab;

[CreateAssetMenu(menuName = "MikanLab/test",fileName = "",order = 10)]
public class NewBehaviourScript : ScriptableObject
{
    public PortDictionary pdt = new();
    public List<RootNode> portDictDrawer = new();

}
