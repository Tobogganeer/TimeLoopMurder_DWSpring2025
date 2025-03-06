using System.Collections;
using System.Collections.Generic;
using Tobo.Attributes;
using UnityEngine;

public class EvidenceObject : MonoBehaviour, IInteractable, ICustomCursor
{
    public enum Type
    {
        Footprints,
        Plant,
        Fireplace,
        GunCabinet,
        Rug,
        None
    }


    // Member variables
    public Type type;
    [ReadOnly] public bool addedToPhotos = false;

    [Space]
    public string photoCaption;
    public Vector2 photoOffset;
    [Range(0.1f, 3f)]
    public float photoSize = 0.5f;

    [HideInInspector]
    public Texture2D capturedImage;


    static Dictionary<Type, EvidenceObject> allEvidence = new Dictionary<Type, EvidenceObject>();

    private void OnEnable()
    {
        allEvidence.Add(type, this);
    }

    private void OnDisable()
    {
        // TODO: Store captured images so we can reload them when the scene reloads?
        allEvidence.Remove(type);
    }

    public static EvidenceObject Get(Type type) => allEvidence[type];

    public void OnClicked()
    {
        // Check to see if we've been collected
        if (!addedToPhotos)
        {
            addedToPhotos = true;
            // TODO: Play sound here
            capturedImage = PhotoCamera.TakePhoto(this);
            PhotoMenu.AddPhoto(type);
        }
    }

    CursorType ICustomCursor.GetCursorType() => !addedToPhotos ? CursorType.Camera : CursorType.InteractHand;

    private void OnDrawGizmosSelected()
    {
        Vector3 cube = new Vector3(photoSize * 2f, photoSize * 2f, 0.2f);
        Gizmos.DrawWireCube(transform.position + (Vector3)photoOffset, cube);
    }
}

public interface ICanHaveEvidenceDroppedOnMe
{
    void HandleEvidence(EvidenceObject.Type type);
}

public static class EvidenceTypeExtensions
{
    public static string GetQuestion(this EvidenceObject.Type type) => type switch
    {
        EvidenceObject.Type.Footprints => "\"What shoes are you wearing?\"",
        EvidenceObject.Type.Plant => "\"Are you strong enough to move the potted plant?\"",
        EvidenceObject.Type.Fireplace => "\"What do you know about the fireplace?\"",
        EvidenceObject.Type.GunCabinet => "\"Can you reach the top of the gub cabinet?\"",
        EvidenceObject.Type.Rug => "\"Did you scuff the rug?\"",
        _ => throw new System.NotImplementedException(),
    };
}