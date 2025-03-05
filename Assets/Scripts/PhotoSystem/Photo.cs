using System.Collections;
using System.Collections.Generic;
using Tobo.Attributes;
using UnityEngine;
using UnityEngine.UI;

public class Photo : MonoBehaviour
{
    [ReadOnly]
    public EvidenceObject.Type type;
    public RawImage image;

    public void Init(EvidenceObject.Type type, Texture2D capturedImage)
    {
        this.type = type;
        image.texture = capturedImage;
    }
}
