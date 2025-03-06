using System.Collections;
using System.Collections.Generic;
using Tobo.Attributes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Tobo.Audio;

public class Photo : MonoBehaviour, IInteractable
{
    [ReadOnly]
    public EvidenceObject.Type type;
    public RawImage image;
    public TMPro.TMP_Text caption;

    bool dragging;

    public void Init(EvidenceObject.Type type)
    {
        this.type = type;
        image.texture = EvidenceObject.Get(type).capturedImage;
        caption.text = EvidenceObject.Get(type).photoCaption;
    }

    void IInteractable.OnClicked()
    {
        // When we start clicking on this object, show us dragging it
        dragging = true;
        DraggedPhotoGUI.Enable(this);
        Sound.PhotoDrag.Play2D();
    }

    private void Update()
    {
        if (dragging && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            dragging = false;
            DraggedPhotoGUI.Disable();
            if (Interactor.CurrentObject != null && Interactor.CurrentObject.TryGetComponent(out ICanHaveEvidenceDroppedOnMe canDropPhoto))
                canDropPhoto.HandleEvidence(type);
            else
                Sound.PhotoDrop.Play2D();
        }    
    }
}
