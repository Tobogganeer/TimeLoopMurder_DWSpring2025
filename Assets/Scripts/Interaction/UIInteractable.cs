using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Makes a UI object interact with the Interactable system
/// </summary>
public class UIInteractable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler
{
    public static UIInteractable CurrentlyHovered;

    public void OnPointerEnter(PointerEventData eventData)
    {
        CurrentlyHovered = this;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Only set to null if we are still hovered
        if (CurrentlyHovered == this)
            CurrentlyHovered = null;
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        // If there is an object on top of us we won't detect OnPointerEnter again
        CurrentlyHovered = this;
    }
}
