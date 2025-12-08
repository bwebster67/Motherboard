using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class StartButton : MonoBehaviour, IPointerClickHandler
{
    public System.Action OnStartButtonClicked;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Start Button Clicked");
        OnStartButtonClicked.Invoke();
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
