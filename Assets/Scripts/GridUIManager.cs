using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GridUIManager : MonoBehaviour
{
    private int gridWidth = 6; // TEMP
    private int gridHeight = 3; //TEMP
    // private List<GameObject> gridRows = new List<GameObject>();
    public Sprite emptyGridSlotSprite;
    public Sprite emptyGridSlotHoverSprite;

    [SerializeField]
    public GameObject gridRowPrefab;
    public GameObject gridSpacePrefab;
    public GameObject emptyComponentSegment;
    public GameObject gridUI;

    public MotherboardGrid motherboardGrid;

    void Awake()
    {
        // gridUI = GameObject.FindGameObjectWithTag("GridUI");
    }

    void Start()
    {
        // gridWidth = motherboardGrid.gridWidth;
        // gridHeight = motherboardGrid.gridHeight;


        // // instantiating rows
        // for (int row = 0; row < gridHeight; row++)
        // {
        //     Debug.Log($"Instantiating row {row}");
        //     GameObject rowObject = Instantiate(original: gridRowPrefab, parent: gridUI.transform);
        //     RectTransform rowRectTransform = rowObject.GetComponent<RectTransform>();
        //     Vector2 rowSize = rowRectTransform.sizeDelta;
        //     rowSize.x = 150 * gridWidth;
        //     rowRectTransform.sizeDelta = rowSize;
        //     gridRows.Add(rowObject);
        // }
    }

    void Update()
    {

    }

    public void UpdateGridUI()
    {
        Debug.Log("Updating GridUI");

        // for (int row = 0; row < gridHeight; row++)
        // {

        //     GameObject currentRow = gridRows[row];
        //     foreach (Transform childTransform in currentRow.transform)
        //     {
        //         // rowChildren.Add(childTransform.gameObject);
        //         Destroy(childTransform.gameObject);
        //     }

        //     // Updating row
        //     for (int col = 0; col < gridWidth; col++)
        //     {

        //         // If spot has a segment in motherboardGrid
        //         if (motherboardGrid.grid[row, col] is not null) {
        //             ComponentInstance component = motherboardGrid.grid[row, col];
        //             Debug.Log($"component test: {component.UIData.name}");

        //             // Add segment
        //             GameObject newComponentSegment = Instantiate(original: emptyComponentSegment, parent: currentRow.transform);
        //             Sprite segmentSprite = component.UIData.sprite;
        //             newComponentSegment.GetComponent<SpriteRenderer>().sprite = segmentSprite;
        //             RectTransform newComponentSegmentTransform = newComponentSegment.GetComponent<RectTransform>();
        //             newComponentSegmentTransform.localScale = new Vector3(120, 120, 0);
        //         }
        //         // Otherwise add a blank
        //         else
        //         {
        //             GameObject gridSpace = Instantiate(original: gridSpacePrefab, parent: currentRow.transform);
        //         }
        //     }
        // }
    }
}
