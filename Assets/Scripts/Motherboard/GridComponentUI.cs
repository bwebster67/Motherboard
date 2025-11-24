using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridComponentUI : MonoBehaviour 
{
    public Vector2Int gridAnchorCoords;
    ComponentUIData componentUIData;
    public int widthInSlots;
    public int heightInSlots;
    // GridUIManager gridUIManager;

    // void Awake()
    // {
        // gridUIManager = FindAnyObjectByType<GridUIManager>();
    // }

    public void Set(ComponentUIData componentUIData)
    {
        this.componentUIData = componentUIData;
        GetComponent<Image>().sprite = componentUIData.sprite;
    }
    public void RefreshSize(GridLayoutGroup grid)
    {
        RectTransform rt = GetComponent<RectTransform>();
        
        float cellX = grid.cellSize.x;
        float cellY = grid.cellSize.y;
        float spaceX = grid.spacing.x;
        float spaceY = grid.spacing.y;

        // Calculate exact size covering the gaps
        float totalWidth = (cellX * widthInSlots) + (spaceX * (widthInSlots - 1));
        float totalHeight = (cellY * heightInSlots) + (spaceY * (heightInSlots - 1));

        rt.sizeDelta = new Vector2(totalWidth, totalHeight);
    }

}
