using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Drawing;

public class GridSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    GridUIManager gridUIManager;
    ComponentUIData componentUIData;
    public Vector2Int gridCoords;
    public GameObject gridComponentUI; // UI component when placed on grid
    public static System.Action<Vector2Int, PointerEventData> onGridSlotPointerDown;
    public static System.Action<Vector2Int, PointerEventData> onGridSlotPointerUp;
    public static System.Action<Vector2Int, PointerEventData> onGridSlotPointerEnter;
    public static System.Action<Vector2Int, PointerEventData> onGridSlotPointerExit;

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
        onGridSlotPointerEnter?.Invoke(gridCoords, pointerEventData);
    } 

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // Change sprite on hover
        GetComponent<Image>().sprite = gridUIManager.emptyGridSlotSprite;
        onGridSlotPointerExit?.Invoke(gridCoords, pointerEventData);
    }

    public void OnPointerDown(PointerEventData pointerEventData)
    {
        onGridSlotPointerDown?.Invoke(gridCoords, pointerEventData);
    }

    public void OnPointerUp(PointerEventData pointerEventData)
    {
        onGridSlotPointerUp?.Invoke(gridCoords, pointerEventData);
    }
}
