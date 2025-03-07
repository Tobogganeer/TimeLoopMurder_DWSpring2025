using System.Collections;
using System.Collections.Generic;
using Tobo.Attributes;
using Tobo.Audio;
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
        None,
        Safe,
        OldWill,
        ChangedWill
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
    static HashSet<Type> capturedEvidence = new HashSet<Type>();

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
            Loop.TakeAction();

            capturedEvidence.Add(type);
            addedToPhotos = true;
            //Sound.Select.Play2D();
            Sound.Camera_shutter.Play2D();
            Sound.DelayedScribble.Play2D();
            AddToPhotoMenu();
        }
    }

    public void AddToPhotoMenu()
    {
        capturedImage = PhotoCamera.TakePhoto(this);
        PhotoMenu.AddPhoto(type);
    }

    public static void LoadPhotosFromLastLoop()
    {
        foreach (Type type in capturedEvidence)
        {
            allEvidence[type].addedToPhotos = true;
            allEvidence[type].AddToPhotoMenu();
        }
    }

    CursorType ICustomCursor.GetCursorType() => !addedToPhotos ? CursorType.Camera : CursorType.Default;

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
        EvidenceObject.Type.GunCabinet => "\"Do you know what's in the cabinet?\"",
        EvidenceObject.Type.Rug => "\"Did you scuff the rug?\"",
        EvidenceObject.Type.Safe => "\"Do you know how to get into the safe?\"",
        EvidenceObject.Type.OldWill => "\"Doesn't Dad have a will?\"",
        EvidenceObject.Type.ChangedWill => "\"Do you know that Dad is changing his will?\"",
        _ => throw new System.NotImplementedException(),
    };

    public static string GetDadMotiveComment(this EvidenceObject.Type type) => type switch
    {
        EvidenceObject.Type.Footprints => "Because my floors are dirty?",
        EvidenceObject.Type.Plant => "Because of that plant",
        EvidenceObject.Type.Fireplace => "Because of my fireplace?",
        EvidenceObject.Type.GunCabinet => "Because of my guns?",
        EvidenceObject.Type.Rug => "Because of my rug?",
        EvidenceObject.Type.Safe => "Because of something in my safe?",
        EvidenceObject.Type.OldWill => "The will on my desk? That isn't even current...",
        EvidenceObject.Type.ChangedWill => "The changes I made to my will? Actually...",
        _ => throw new System.NotImplementedException(),
    };

    public static string GetDadMeansComment(this EvidenceObject.Type type) => type switch
    {
        EvidenceObject.Type.Footprints => "Step on me?",
        EvidenceObject.Type.Plant => "Hit me with a plant?",
        EvidenceObject.Type.Fireplace => "Burn me alive?",
        EvidenceObject.Type.GunCabinet => "Someone broke into my gun cabinet?",
        EvidenceObject.Type.Rug => "What? Hit me with... a rug?",
        EvidenceObject.Type.Safe => "There isn't anything in my safe that could hurt me?",
        EvidenceObject.Type.OldWill => "Are they going to tear up my will?",
        EvidenceObject.Type.ChangedWill => "What, are they going to wait until I drop dead? Hah.",
        _ => throw new System.NotImplementedException(),
    };
}