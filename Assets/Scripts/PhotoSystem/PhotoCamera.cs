using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PhotoCamera : MonoBehaviour
{
    private static PhotoCamera instance;
    private void Awake()
    {
        instance = this;
    }

    public Camera renderCamera;
    public RenderTexture rt;

    public static Texture2D TakePhoto(Vector3 position, float size)
    {
        // Set cam position and size
        instance.transform.position = position;
        instance.renderCamera.orthographicSize = size;

        // Set up camera and render to RT
        instance.renderCamera.targetTexture = instance.rt;
        RenderTexture.active = instance.rt;
        instance.renderCamera.Render();

        // Read RT into a new texture and return it
        Texture2D texture = new Texture2D(instance.rt.width, instance.rt.height, TextureFormat.RGBA32, false, true);
        texture.ReadPixels(new Rect(0, 0, instance.rt.width, instance.rt.height), 0, 0);
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.Apply();
        return texture;
    }

    public static Texture2D TakePhoto(EvidenceObject evidence)
    {
        // Set our position and size to look at the evidence
        return TakePhoto(evidence.transform.position + (Vector3)evidence.photoOffset, evidence.photoSize);
    }
}
