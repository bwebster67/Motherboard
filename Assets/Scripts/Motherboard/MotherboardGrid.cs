using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MotherboardGrid : MonoBehaviour
{
    public ComponentInstance[,] grid;
    public GridUIManager gridUIManager;
    public int gridWidth = 6;
    public int gridHeight = 3;
    // private Dictionary<ComponentInstance, Vector2Int> gridComponents;
    public ComponentUICreator componentUICreator;
    public PlayerWeaponManager playerWeaponManager;
    public GameObject testWeaponController;


    void Start()
    {
        grid = new ComponentInstance[gridHeight, gridWidth];

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

        // Adding to Grid UI 

        // Getting the weapon controller, temporarily stored in the game object
        WeaponComponentInstance testComponentInstance = testWeaponController.GetComponent<WeaponComponentInstance>();
        
        // Backend
        PlaceComponent(component: testComponentInstance, position: new Vector2Int(0, 0));
        // UI (moved to the end of PlaceComponent)
        // gridUIManager.PlaceComponentUI(testComponentInstance.UIData, gridCoords: new Vector2Int(0, 0));


        // Adding weapon functionality

        // just commented out so that it stops logging stuff
        // playerWeaponManager.AddWeapon(testWeaponController);

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

        gridUIManager.RemoveComponentUI(component);
        Debug.Log($"Component Removed at ({anchorPosition.x}, {anchorPosition.y})");
        Debug.Log(GridString());
        return true;

    }

    // float GetTotalWattage()
    // {
        
    // }

}
