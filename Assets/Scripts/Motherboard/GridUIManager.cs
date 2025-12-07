using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Versioning;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class GridUIManager : MonoBehaviour
{
    private int gridWidth = 6; // TEMP
    private int gridHeight = 3; //TEMP
    public GameObject gridSlotPrefab;
    public Sprite emptyGridSlotSprite;
    public Sprite emptyGridSlotHoverSprite;
    public GameObject[,] gridUISlots;

    [SerializeField]
    public GameObject emptyComponentUI;
    public GameObject gridUI;
    public GameObject gridComponentSprites;

    public MotherboardGrid motherboardGrid;


    // Component Selection
    private ComponentSelectionUIManager componentSelectionUIManager;


    void Awake()
    {
        gridUISlots = new GameObject[gridHeight, gridWidth];
        if (componentSelectionUIManager == null) componentSelectionUIManager = FindAnyObjectByType<ComponentSelectionUIManager>(); 
    }

    void Start()
    {
        GenerateGrid();
        RectTransform gridRect = gridUI.GetComponent<RectTransform>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(gridRect);
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

    public void RemoveComponentUI(ComponentPointer componentPointer)
    {
        GameObject componentGO = componentPointer.componentController;
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
                    GridSlot gridSlot = gridUISlots[componentPointer.gridAnchorPosition.x, componentPointer.gridAnchorPosition.y].GetComponent<GridSlot>();
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
