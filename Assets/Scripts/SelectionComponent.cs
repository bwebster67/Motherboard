using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionComponent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject componentPrefab;
    public ComponentUIData componentUIData;
    public TextMeshProUGUI componentLabel;
    public GameObject filledSegmentPrefab;
    public GameObject emptySegmentPrefab;
    public GameObject componentGrid;
    public GridUIManager gridUIManager;

    // Events
    public static System.Action<GameObject> onSelectionSlotPointerDown;
    public static System.Action<GameObject> onSelectionSlotPointerUp;


    public void Awake()
    {
        gridUIManager = FindAnyObjectByType<GridUIManager>();
    }

    public void AssignComponent(GameObject componentPrefab)
    {
        this.componentPrefab = componentPrefab;
        componentUIData = componentPrefab.GetComponent<ComponentInstance>().UIData;
        InitializeComponentGrid();
    }

    void InitializeComponentGrid()
    {
        componentLabel.text = componentPrefab.GetComponent<ComponentInstance>().UIData.componentName;

        // fill grid with non-empty and empty segments based on shape from componentUIData
        int numRows = componentUIData.shape.Count; 
        int numCols = componentUIData.shape[0].cols.Count; 

        for (int row = 0; row < numRows; row++)
        {
            for (int col = 0; col < numCols; col++)
            {
                if (componentUIData.shape[row].cols[col])
                {
                    Instantiate(original: filledSegmentPrefab, parent: componentGrid.transform);
                }
                else
                {
                    Instantiate(original: emptySegmentPrefab, parent: componentGrid.transform);
                }
            }
        }
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        onSelectionSlotPointerDown?.Invoke(componentPrefab);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        onSelectionSlotPointerUp?.Invoke(componentPrefab);
    }
}
