using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoTesting : MonoBehaviour
{
    public UnityEngine.UI.RawImage rawImage;

    private void Update()
    {
        if (UnityEngine.InputSystem.Keyboard.current.spaceKey.wasPressedThisFrame)
            rawImage.texture = PhotoCamera.TakePhoto(transform.position, 4);
    }
}
