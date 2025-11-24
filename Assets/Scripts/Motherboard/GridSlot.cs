using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    GridUIManager gridUIManager;
    ComponentUIData componentUIData;
    public Vector2Int gridCoords;
    public GameObject gridComponentUI; // UI component when placed on grid

    void Awake()
    {
        gridUIManager = FindAnyObjectByType<GridUIManager>();
    }

    public void Set(ComponentUIData componentUIData, GameObject gridComponentUI)
    {
        this.componentUIData = componentUIData;
        this.gridComponentUI = gridComponentUI;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // Change sprite on hover
        GetComponent<Image>().sprite = gridUIManager.emptyGridSlotHoverSprite;
        Debug.Log($"Hovering over ({gridCoords.x}, {gridCoords.y})");
        gridUIManager.OnGridSlotEnter(gridCoords);
    } 

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // Change sprite on hover
        GetComponent<Image>().sprite = gridUIManager.emptyGridSlotSprite;
        gridUIManager.OnGridSlotExit(gridCoords);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        gridUIManager.OnGridSlotMouseDown(gridCoords);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        gridUIManager.OnGridSlotMouseUp(gridCoords);
    }
}
