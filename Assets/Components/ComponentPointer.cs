using UnityEngine;

public class ComponentPointer : ScriptableObject 
{
    // I want this to be stored in the grid instead of an actual componentInstance
    public GameObject componentController;
    public Vector2Int gridPosition; // position of THIS pointer
    public Vector2Int gridAnchorPosition; // position of the ANCHOR of the component this obj points to

    // Constructor
    public void Init(GameObject componentController, Vector2Int gridPosition, Vector2Int gridAnchorPosition)
    {
        this.componentController = componentController;
        this.gridPosition = gridPosition;
        this.gridAnchorPosition = gridAnchorPosition;
    }

    public void UpdatePosition(Vector2Int? gridPosition, Vector2Int? gridAnchorPosition)
    {
        if (gridPosition != null)
        {
            this.gridPosition = gridPosition.Value;
        }
        if (gridAnchorPosition != null)
        {
            this.gridAnchorPosition = gridAnchorPosition.Value;
        }
    }

}