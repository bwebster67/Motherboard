using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class MotherboardGrid : MonoBehaviour
{
    public GameObject[,] grid;
    private List<Vector2Int> gridComponentAnchors;
    public GridUIManager gridUIManager;
    public int gridWidth = 6;
    public int gridHeight = 3;
    public ComponentFactory componentFactory;
    public PlayerComponentManager playerComponentManager;



    void Start()
    {
        grid = new GameObject[gridHeight, gridWidth];
        gridComponentAnchors = new List<Vector2Int>(); 

        // Making grid start null
        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                grid[row, col] = null;
            }
        }

        Debug.Log("Instantiated new MotherboardGrid");


        Debug.Log(GridString());


        // Components must be passed around (at least for now) by the weaponController gameObject
        // I added gridComponentAnchors, but with PlayerComponentManager that might not be necessary right now?
        // componentFactory.GetComponentChoices();

        // GameObject startingComponent = componentFactory.GetComponent(0);
        // AddComponentEverywhere(startingComponent, new Vector2Int(0, 0));

        ReloadMotherboard();

        Debug.Log(GridString());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            print(GridString());
        }
    }

    public bool PlaceComponentEverywhere(GameObject componentGO, Vector2Int position)
    {
        GameObject componentGOInstance = Instantiate(componentGO);
        // Backend and UI
        if (PlaceComponent(componentGO: componentGOInstance, position: position))
        {
            // NEED TO CHANGE WHEN I ADD NON-WEAPONS
            // playerComponentManager.AddWeapon(componentGOInstance);
            return true;
        }
        else
        {
            Destroy(componentGOInstance);
            return false;
        }
    }

    public bool RemoveComponentEverywhere(GameObject componentGO, Vector2Int position)
    {
        if (RemoveComponent(position))
        {
            // playerComponentManager.RemoveWeapon(componentGO);
            return true;
        }
        return false;
    }

    public void ReloadMotherboard()
    {
        // NOT USEFUL YET
        // Takes the components stored in the motherboardGrid and gives the player their functionality
        Debug.Log("Reloading Motherboard.");

        playerComponentManager.ClearComponents();

        foreach (Vector2Int anchor in gridComponentAnchors)
        {
            // add component functionality
            GameObject componentGO = grid[anchor.x, anchor.y];
            playerComponentManager.AddWeapon(componentGO);

        }
    }

    string GridString()
    {
        string output = "";
        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                if (grid[row, col])
                {
                    output += $"[x] ";
                }
                else
                {
                    output += $"[  ] ";
                }
            }
            output += "\n";
        }

        return output;
    }

    public bool MoveComponent(GameObject componentGO, Vector2Int newPosition)
    {
        ComponentInstance componentInstance = componentGO.GetComponent<ComponentInstance>();
        int componentRows = componentInstance.UIData.shape.Count;
        int componentCols = componentInstance.UIData.shape[0].cols.Count;
        List<ComponentShapeRow> componentShape = componentInstance.UIData.shape;
        Vector2Int oldPosition = componentInstance.anchorPosition;

        // Removing each segment from grid 
        for (int row = 0; row < componentRows; row++)
        {
            for (int col = 0; col < componentCols; col++)
            {
                if (componentShape[row].cols[col])
                {
                    Vector2Int newPos = oldPosition + new Vector2Int(row, col);
                    grid[newPos.x, newPos.y] = null;
                }
            }
        }

        // Place in UI
        gridUIManager.PlaceComponentUI(componentInstance.UIData, gridCoords: newPosition);

        // Save in gridComponentAnchors
        gridComponentAnchors.Add(newPosition);

        // RemoveComponentEverywhere(componentGO, componentInstance.gridPosition);
        // PlaceComponentEverywhere(componentGO, newPosition);
        print(GridString());
        return true;
    }

    public bool CanPlaceComponent(GameObject componentGO, Vector2Int position)
    // returns True if the passed component can be placed in the passed location, False otherwise
    {
        // for segment in component_shape
        ComponentInstance component = componentGO.GetComponent<ComponentInstance>();
        List<ComponentShapeRow> componentShape = component.UIData.shape;
        int componentRows = component.UIData.shape.Count;
        int componentCols = component.UIData.shape[0].cols.Count;

        if ((position.x < 0) || (position.y < 0) || (position.x >= gridHeight) || (position.y >= gridWidth))
        {
            Debug.LogError($"Cannot place component at position {position}.");
            return false;
        }

        // Populating componentSegmentPositions
        for (int row = 0; row < componentRows; row++)
        {
            for (int col = 0; col < componentCols; col++)
            {
                if (componentShape[row].cols[col])
                {
                    Vector2Int newPos = position + new Vector2Int(row, col);

                    // Check to see if a segment is out of bounds of the grid
                    if ((newPos.x >= gridHeight) || (newPos.y >= gridWidth))
                    {
                        Debug.Log($"Segment out of bounds at [{newPos.x}, {newPos.y}].");
                        return false;
                    }

                    // If spot is taken by another component
                    if (grid[newPos.x, newPos.y] is not null)
                    {
                        // Check to see if new spot could safely overlap with old one
                        if (grid[newPos.x, newPos.y] != componentGO)
                        {
                            Debug.Log($"Segment blocked by another component at [{newPos.x}, {newPos.y}].");
                            return false;
                        }
                    }
                }
            }
        }

        return true;
    }

    public bool PlaceComponent(GameObject componentGO, Vector2Int position)
    // places the passed component at the passed location
    {
        ComponentInstance component = componentGO.GetComponent<ComponentInstance>();
        List<ComponentShapeRow> componentShape = component.UIData.shape;
        int componentRows = component.UIData.shape.Count;
        int componentCols = component.UIData.shape[0].cols.Count;

        if (!CanPlaceComponent(componentGO, position))
        {
            Debug.LogError($"Cannot place component at position {position}.");
            return false;
        }
        component.gridPosition = position;
        component.anchorPosition = position;
        
        for (int row = 0; row < componentRows; row++)
        {
            for (int col = 0; col < componentCols; col++)
            {
                if (componentShape[row].cols[col])
                {
                    Vector2Int newPos = position + new Vector2Int(row, col);
                    grid[newPos.x, newPos.y] = componentGO;
                }
            }
        }

        // Place in UI
        gridUIManager.PlaceComponentUI(component.UIData, gridCoords: position);

        // Save in gridComponentAnchors
        gridComponentAnchors.Add(position);
        
        Debug.Log($"Component placed at ({position.x}, {position.y})");
        Debug.Log(GridString());
        return true;
    }

    public bool RemoveComponent(Vector2Int anchorPosition)
    {
        GameObject componentGO = grid[anchorPosition.x, anchorPosition.y];
        if (componentGO == null)
        {
            Debug.LogError($"No component to remove at anchor ({anchorPosition.x}, {anchorPosition.y})");
            return false;
        }

        ComponentInstance componentInstance = componentGO.GetComponent<ComponentInstance>();   
        List<ComponentShapeRow> componentShape = componentInstance.UIData.shape;
        int componentRows = componentInstance.UIData.shape.Count;
        int componentCols = componentInstance.UIData.shape[0].cols.Count;

        for (int row = 0; row < componentRows; row++)
        {
            for (int col = 0; col < componentCols; col++)
            {
                if (componentShape[row].cols[col])
                {
                    grid[anchorPosition.x + row, anchorPosition.y + col] = null;
                }
            }
        }

        // Remove from UI
        gridUIManager.RemoveComponentUI(componentGO);

        // Remove from gridComponentAnchors
        gridComponentAnchors.Remove(anchorPosition);

        Debug.Log($"Component Removed at ({anchorPosition.x}, {anchorPosition.y})");
        Debug.Log(GridString());
        return true;

    }

}
