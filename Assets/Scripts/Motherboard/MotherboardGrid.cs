using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MotherboardGrid : MonoBehaviour
{
    public ComponentInstance[,] grid;
    private List<Vector2Int> gridComponentAnchors;
    public GridUIManager gridUIManager;
    public int gridWidth = 6;
    public int gridHeight = 3;
    public ComponentUICreator componentUICreator;
    public PlayerComponentManager playerComponentManager;
    public GameObject testWeaponController;


    void Start()
    {
        grid = new ComponentInstance[gridHeight, gridWidth];
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


        // Flow for adding a weapon component --------

        // Getting the weapon controller, temporarily stored in the game object
        WeaponComponentInstance testComponentInstance = testWeaponController.GetComponent<WeaponComponentInstance>();
        
        // Backend and UI
        PlaceComponent(component: testComponentInstance, position: new Vector2Int(0, 0));

        // Adding weapon functionality
        playerComponentManager.AddWeapon(testWeaponController);

        // --------------------------------------------

        ReloadMotherboard();

        Debug.Log(GridString());
    }

    void Update()
    {

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

    public void ReloadMotherboard()
    {
        // NOT USEFUL YET
        // Takes the components stored in the motherboardGrid and gives the player their functionality
        Debug.Log("Reloading Motherboard.");

        foreach (Vector2Int anchor in gridComponentAnchors)
        {
            // add component functionality
            ComponentInstance componentInstance = grid[anchor.x, anchor.y];

        }
    }

    bool CanPlaceComponent(ComponentInstance component, Vector2Int position)
    // returns True if the passed component can be placed in the passed location, False otherwise
    {
        // for segment in component_shape
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
                        Debug.Log($"Segment blocked by another component at [{newPos.x}, {newPos.y}].");
                        return false;
                    }
                }
            }
        }

        return true;
    }

    public bool PlaceComponent(ComponentInstance component, Vector2Int position)
    // places the passed component at the passed location
    {
        List<ComponentShapeRow> componentShape = component.UIData.shape;
        int componentRows = component.UIData.shape.Count;
        int componentCols = component.UIData.shape[0].cols.Count;

        if (!CanPlaceComponent(component, position))
        {
            Debug.LogError($"Cannot place component at position {position}.");
            return false;
        }
        component.anchorPosition = position;
        
        for (int row = 0; row < componentRows; row++)
        {
            for (int col = 0; col < componentCols; col++)
            {
                if (componentShape[row].cols[col])
                {
                    Vector2Int newPos = position + new Vector2Int(row, col);
                    grid[newPos.x, newPos.y] = component;

                    // Debug.Log($"Position of component segment [{row}, {col}] in grid is [{newPos.x},{newPos.y}].");
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
        ComponentInstance component = grid[anchorPosition.x, anchorPosition.y];
        if (component == null)
        {
            Debug.LogError($"No component to remove at anchor ({anchorPosition.x}, {anchorPosition.y})");
            return false;
        }

        List<ComponentShapeRow> componentShape = component.UIData.shape;
        int componentRows = component.UIData.shape.Count;
        int componentCols = component.UIData.shape[0].cols.Count;

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
        gridUIManager.RemoveComponentUI(component);

        // Remove from gridComponentAnchors
        gridComponentAnchors.Remove(anchorPosition);

        Debug.Log($"Component Removed at ({anchorPosition.x}, {anchorPosition.y})");
        Debug.Log(GridString());
        return true;

    }

    // float GetTotalWattage()
    // {
        
    // }

}
