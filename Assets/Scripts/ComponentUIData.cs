using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

[CreateAssetMenu(fileName = "ComponentData", menuName = "Scriptable Objects/ComponentData")]
public class ComponentUIData : ScriptableObject
{
    [SerializeField]
    public string componentName;
    [SerializeField]
    public List<ComponentShapeRow> shape = new List<ComponentShapeRow>();
    [SerializeField]
    public Sprite sprite;
}
