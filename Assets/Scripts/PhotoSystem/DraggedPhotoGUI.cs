using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;
using UnityEngine.InputSystem;

public class DraggedPhotoGUI : MonoBehaviour
{
    private static DraggedPhotoGUI instance;
    private void Awake()
    {
        instance = this;
    }

    public Photo photo;
    RectTransform rt;

    private void Start()
    {
        rt = photo.GetComponent<RectTransform>();
        photo.gameObject.SetActive(false);
    }

    private void LateUpdate()
    {
        if (photo.gameObject.activeInHierarchy)
            rt.position = Mouse.current.position.value;// + dragOffset;
    }

    public static void Enable(Photo photo)
    {
        instance.photo.Init(photo.type, photo.rawTexture);
    }

    public static void Disable()
    {
        instance.photo.gameObject.SetActive(false);
    }
}
