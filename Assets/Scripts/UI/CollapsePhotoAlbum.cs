using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CollapsePhotoAlbum : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
{
    public Animator OpenCloser;
    public bool opened = false;

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        opened = !opened;
        OpenCloser.SetBool("Opened", opened);
    }
}
