using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoMenu : MonoBehaviour
{
    private static PhotoMenu instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject photoPrefab;
    public Transform photoHolder;

    const float RotationRange = 10f;
    
    public static Photo AddPhoto(EvidenceObject.Type type, Texture2D capturedImage)
    {
        // Spawn new photo with slight random rotation
        GameObject newPhotoObject = Instantiate(instance.photoPrefab, Vector3.zero,
            Quaternion.Euler(0, 0, Random.Range(-RotationRange, RotationRange)));
        // Get aligned automatically by LayoutGroup
        newPhotoObject.transform.SetParent(instance.photoHolder, false);

        Photo photo = newPhotoObject.GetComponent<Photo>();
        photo.Init(type, capturedImage);
        return photo;
    }
}
