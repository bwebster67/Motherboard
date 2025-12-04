using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class GridUIManager : MonoBehaviour
{
    private int gridWidth = 6; // TEMP
    private int gridHeight = 3; //TEMP
    // private List<GameObject> gridRows = new List<GameObject>();
    public GameObject gridSlotPrefab;
    public Sprite emptyGridSlotSprite;
    public Sprite emptyGridSlotHoverSprite;
    public GameObject[,] gridUISlots;

    [SerializeField]
    public GameObject emptyComponentUI;
    public GameObject gridUI;
    public GameObject gridComponentSprites;

    public MotherboardGrid motherboardGrid;

    // Dragging
    private GameObject draggedComponent;
    private Vector2Int dragOffset;
    private Vector2Int? currentHoveredSlotCoords;


    void Awake()
    {
        gridUISlots = new GameObject[gridHeight, gridWidth];
    }

    void Start()
    {
        GenerateGrid();
        RectTransform gridRect = gridUI.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridRect);
    }


    public void OnSelectionSlotMouseDown(GameObject componentPrefab)
    {
        Debug.Log("SelectionSlot Mouse Down");
        draggedComponent = componentPrefab;
        // dragOffset = new Vector2Int(0, 0);
    }

    public void OnSelectionSlotMouseUp(GameObject componentPrefab)
    {
        Debug.Log("SelectionSlot Mouse Up");
        if (draggedComponent == null) return;
        Vector2Int targetAnchor = currentHoveredSlotCoords.Value;
        if (motherboardGrid.AddComponentEverywhere(draggedComponent, targetAnchor))
        {
            Debug.Log($"Component successfully placed at ({targetAnchor.x}, {targetAnchor.y})");
        }
        draggedComponent = null;
        dragOffset = new Vector2Int(0, 0);
    }


    public void OnGridSlotMouseDown(Vector2Int gridCoords)
    {
        Debug.Log($"Mouse down on slot ({gridCoords.x}, {gridCoords.y})");

        GameObject componentGO = motherboardGrid.grid[gridCoords.x, gridCoords.y];
        ComponentInstance componentInstance = componentGO.GetComponent<ComponentInstance>();
        if (componentGO == null)
        {
            Debug.Log($"No component to drag in slot ({gridCoords.x}, {gridCoords.y})");
            return;
        }
        motherboardGrid.RemoveComponent(componentInstance.anchorPosition);

        dragOffset = gridCoords - componentInstance.anchorPosition;
        draggedComponent = componentGO;
    }
    public void OnGridSlotMouseUp(Vector2Int gridCoords)
    {
        Debug.Log($"Mouse up on slot ({gridCoords.x}, {gridCoords.y})");
        if (draggedComponent == null) return;

        if (currentHoveredSlotCoords == null)
        {
            // Component was dropped outside of the grid, do not place
            Debug.Log("Component dropped out of grid.");
            motherboardGrid.AddComponentEverywhere(draggedComponent, gridCoords- dragOffset);
            draggedComponent = null;
            dragOffset = new Vector2Int(0, 0);
            return;
        }

        Vector2Int targetAnchor = currentHoveredSlotCoords.Value - dragOffset;
        Debug.Log($"targetAnchor: ({targetAnchor.x}, {targetAnchor.y}) = gridCoords ({gridCoords.x}, {gridCoords.y}) - dragOffset ({dragOffset.x}, {dragOffset.y})");
        if (motherboardGrid.AddComponentEverywhere(draggedComponent, targetAnchor))
        {
            Debug.Log($"Component successfully placed at ({targetAnchor.x}, {targetAnchor.y})");
        }
        else
        {
            motherboardGrid.AddComponentEverywhere(draggedComponent, gridCoords - dragOffset);
        }
        draggedComponent = null;
        dragOffset = new Vector2Int(0, 0);
    }

    public void OnGridSlotEnter(Vector2Int gridCoords)
    {
        currentHoveredSlotCoords = gridCoords;
    }
    public void OnGridSlotExit(Vector2Int gridCoords)
    {
        currentHoveredSlotCoords = null;
    }

    void GenerateGrid()
    // Fills the grid with empty slots
    {
        for (int r = 0; r < gridHeight; r++)
        {
            for (int c = 0; c < gridWidth; c++)
            {
                GameObject gridSlot = Instantiate(original: gridSlotPrefab, parent: gridUI.transform);
                gridSlot.GetComponent<GridSlot>().gridCoords = new Vector2Int(r, c);
                gridUISlots[r, c] = gridSlot;
            }
        }
    }

    public void PlaceComponentUI(ComponentUIData componentUIData, Vector2Int gridCoords) 
    {
        // Get grid slot from coords
        GridSlot gridItem = gridUISlots[gridCoords.x, gridCoords.y].GetComponent<GridSlot>();
        
        // Creating object for sprite
        GameObject gridComponentUIGO = Instantiate(original: emptyComponentUI, parent: gridComponentSprites.transform);
        gridComponentUIGO.GetComponent<Image>().sprite = componentUIData.sprite;
        GridComponentUI gridComponentUI = gridComponentUIGO.GetComponent<GridComponentUI>();
        gridComponentUI.heightInSlots = componentUIData.height;
        gridComponentUI.widthInSlots = componentUIData.width;

        gridComponentUI.gridAnchorCoords = gridCoords;
        RectTransform anchorSlotRectTransform = gridUISlots[gridCoords.x, gridCoords.y].GetComponent<RectTransform>(); 
        RectTransform gridComponentUIRectTransform = gridComponentUI.GetComponent<RectTransform>();

        // cant get the anchorSlotRectTransform.position here when it disabled at start...
        gridComponentUIRectTransform.position = anchorSlotRectTransform.position;
        GridLayoutGroup gridLayoutGroup = gridUI.GetComponent<GridLayoutGroup>();

        gridComponentUI.RefreshSize(gridLayoutGroup);
        gridItem.Set(componentUIData, gridComponentUIGO);
    }

    public void RemoveComponentUI(GameObject componentGO)
    {
        ComponentInstance componentInstance = componentGO.GetComponent<ComponentInstance>();

        // and remove the UI component
        List<ComponentShapeRow> componentShape = componentInstance.UIData.shape;
        int componentRows = componentInstance.UIData.shape.Count;
        int componentCols = componentInstance.UIData.shape[0].cols.Count;

        for (int row = 0; row < componentRows; row++)
        {
            for (int col = 0; col < componentCols; col++)
            {
                if (componentShape[row].cols[col])
                {
                    GridSlot gridSlot = gridUISlots[componentInstance.anchorPosition.x, componentInstance.anchorPosition.y].GetComponent<GridSlot>();
                    if (gridSlot.gridComponentUI != null)
                    {
                        Destroy(gridSlot.gridComponentUI);
                    }
                    gridSlot.Set(null, null);
                }
            }
        }
    }


}
