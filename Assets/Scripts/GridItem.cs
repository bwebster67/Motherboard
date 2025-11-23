using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridItem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    GridUIManager gridUIManager;
    ComponentUIData componentUIData;

    void Awake()
    {
        gridUIManager = FindAnyObjectByType<GridUIManager>();
    }

    void Set(ComponentUIData componentUIData)
    {
        this.componentUIData = componentUIData;
        
        // Update sprite 
        GetComponent<Image>().sprite = componentUIData.sprite;
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        // Change sprite on hover
        if (componentUIData is null)
        {
            GetComponent<Image>().sprite = gridUIManager.emptyGridSlotHoverSprite;
        } 
    } 

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        // Change sprite on hover
        if (componentUIData is null)
        {
            GetComponent<Image>().sprite = gridUIManager.emptyGridSlotSprite;
        }
    }
}
