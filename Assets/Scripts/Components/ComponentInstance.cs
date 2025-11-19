using UnityEngine;

public abstract class ComponentInstance : MonoBehaviour
{
    public ComponentUIData UIData;
    protected int currentLevel = 1;
    protected Vector2Int gridPosition; // top-left aligned

    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

}
