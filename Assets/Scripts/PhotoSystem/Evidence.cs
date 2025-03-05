using System.Collections;
using System.Collections.Generic;
using Tobo.Attributes;
using UnityEngine;

public class Evidence : MonoBehaviour, IInteractable
{
    public enum Type
    {
        Footprints,
        Plant
    }


    public Type type;
    [ReadOnly] public bool addedToPhotos = false;

    [Space]
    public Vector2 photoOffset;
    [Range(0.1f, 3f)]
    public float photoSize = 0.5f;


    static Dictionary<Type, Evidence> allEvidence = new Dictionary<Type, Evidence>();

    private void Awake()
    {
        allEvidence.Add(type, this);
    }

    public static Evidence Get(Type type) => allEvidence[type];

    public void OnClicked()
    {
        // Check to see if we've been collected
        if (!addedToPhotos)
        {
            addedToPhotos = true;
            // TODO: Play sound here
        }
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 cube = new Vector3(photoSize * 2f, photoSize * 2f, 0.2f);
        Gizmos.DrawWireCube(transform.position + (Vector3)photoOffset, cube);
    }
}
