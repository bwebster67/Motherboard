using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MotherboardGrid : MonoBehaviour
{
    public ComponentInstance[,] grid;
    public GridUIManager gridUIManager;
    public int gridWidth = 4;
    public int gridHeight = 4;
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
        WeaponComponentInstance testComponentInstance = testWeaponController.GetComponent<WeaponComponentInstance>();
        PlaceComponent(component: testComponentInstance, position: new Vector2Int(0, 0));

        // Adding weapon functionality
        playerWeaponManager.AddWeapon(testWeaponController);

        // ComponentInstance testComponent1 = new ComponentInstance();
        // testComponent1.UIData = componentCreator.GetComponent(1);
        // PlaceComponent(component: testComponent1, position: new Vector2Int(0, 2));

        // ComponentInstance testComponent2 = new ComponentInstance();
        // testComponent2.UIData = componentCreator.GetComponent(2);
        // PlaceComponent(component: testComponent2, position: new Vector2Int(2, 0));

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
                output += $"[{grid[row, col]}] ";
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

    void PlaceComponent(ComponentInstance component, Vector2Int position)
    // places the passed component at the passed location
    {
        List<ComponentShapeRow> componentShape = component.UIData.shape;
        int componentRows = component.UIData.shape.Count;
        int componentCols = component.UIData.shape[0].cols.Count;

        if (!CanPlaceComponent(component, position))
        {
            Debug.LogError($"Cannot place component at position {position}.");
            return;
        }
        
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

        gridUIManager.UpdateGridUI();

    }

    // float GetTotalWattage()
    // {
        
    // }

}
