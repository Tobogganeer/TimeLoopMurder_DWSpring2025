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

    public static Texture2D TakePhoto()
    {
        instance.renderCamera.targetTexture = instance.rt;
        RenderTexture.active = instance.rt;
        instance.renderCamera.Render();

        Texture2D texture = new Texture2D(instance.rt.width, instance.rt.height, TextureFormat.RGBA32, false, false);
        texture.ReadPixels(new Rect(0, 0, instance.rt.width, instance.rt.height), 0, 0);
        texture.Apply();
        return texture;
    }
}
