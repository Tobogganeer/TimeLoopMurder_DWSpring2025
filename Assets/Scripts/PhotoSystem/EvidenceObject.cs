using System.Collections;
using System.Collections.Generic;
using Tobo.Attributes;
using UnityEngine;

public class EvidenceObject : MonoBehaviour, IInteractable
{
    public enum Type
    {
        Footprints,
        Plant,
        Fireplace
    }


    // Member variables
    public Type type;
    [ReadOnly] public bool addedToPhotos = false;

    [Space]
    public string photoCaption;
    public Vector2 photoOffset;
    [Range(0.1f, 3f)]
    public float photoSize = 0.5f;


    static Dictionary<Type, EvidenceObject> allEvidence = new Dictionary<Type, EvidenceObject>();

    private void Awake()
    {
        allEvidence.Add(type, this);
    }

    public static EvidenceObject Get(Type type) => allEvidence[type];

    public void OnClicked()
    {
        // Check to see if we've been collected
        if (!addedToPhotos)
        {
            addedToPhotos = true;
            // TODO: Play sound here
            PhotoMenu.AddPhoto(type, PhotoCamera.TakePhoto(this));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 cube = new Vector3(photoSize * 2f, photoSize * 2f, 0.2f);
        Gizmos.DrawWireCube(transform.position + (Vector3)photoOffset, cube);
    }
}
