using System.Collections;
using System.Collections.Generic;
using Tobo.Attributes;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Tobo.Audio;

public class Photo : MonoBehaviour, IInteractable
{
    public EvidenceObject.Type type;
    public RawImage image;
    public TMPro.TMP_Text caption;

    bool dragging;

    static bool anyPhotoPickedUpYet;

    const float StartScale = 0.8f;
    const float ShrinkSpeed = 3f;

    private void Start()
    {
        transform.localScale = Vector3.one * StartScale;
        Init(type);
    }

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

        // First time picking up pic - give a little tutorial
        if (!anyPhotoPickedUpYet)
        {
            anyPhotoPickedUpYet = true;
            PopUp.Show("Drag a photo to an NPC to ask them about it");
        }
    }

    private void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * ShrinkSpeed);

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
