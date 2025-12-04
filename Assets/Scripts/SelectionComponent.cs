using System.Data;
using TMPro;
using UnityEngine;

public class SelectionComponent : MonoBehaviour
{
    public GameObject componentPrefab;
    public ComponentUIData componentUIData;
    public TextMeshProUGUI componentLabel;
    public GameObject filledSegmentPrefab;
    public GameObject emptySegmentPrefab;
    public GameObject componentGrid;

    public void AssignComponent(GameObject componentPrefab)
    {
        this.componentPrefab = componentPrefab;
        componentUIData = componentPrefab.GetComponent<ComponentInstance>().UIData;
        InitializeComponentGrid();
    }
    void InitializeComponentGrid()
    {
        componentLabel.text = componentPrefab.GetComponent<ComponentInstance>().componentName;

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
}
