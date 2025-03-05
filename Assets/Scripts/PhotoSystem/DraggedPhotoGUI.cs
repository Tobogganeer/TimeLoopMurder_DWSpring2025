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

    public static bool CurrentlyHoldingEvidence { get; private set; }
    public static EvidenceObject.Type CurrentEvidenceType { get; private set; }

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
        instance.photo.gameObject.SetActive(true);
        instance.photo.Init(photo.type);
        CurrentlyHoldingEvidence = true;
        CurrentEvidenceType = photo.type;
    }

    public static void Disable()
    {
        instance.photo.gameObject.SetActive(false);
        CurrentlyHoldingEvidence = false;
    }
}
