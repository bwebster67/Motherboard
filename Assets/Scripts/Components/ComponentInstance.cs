using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ComponentInstance : MonoBehaviour
{
    public ComponentUIData UIData;
    protected int currentLevel = 1;
    public Vector2Int gridPosition; // top-left aligned
    public Vector2Int anchorPosition; // top-left 

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    // public Vector2Int GetAnchorPos()
    // {
    //     return new Vector2Int();
    // } 

}
