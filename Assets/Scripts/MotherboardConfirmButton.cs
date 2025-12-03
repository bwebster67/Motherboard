using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class MotherboardConfirmButton : MonoBehaviour, IPointerClickHandler
{
    public System.Action OnConfirmButtonClicked;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Confirm Button Clicked");
        OnConfirmButtonClicked.Invoke();
    }


    // void OnPointerEnter(PointerEventData)
    // {
    //     // change sprite to hover sprite
    //     return;
    // }

    // void OnPointerExit(PointerEventData)
    // {
    //     // change sprite to non-hover sprite
    //     return;
    // }


}
