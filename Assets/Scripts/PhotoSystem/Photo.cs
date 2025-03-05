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
    public TMPro.TMP_Text caption;

    [HideInInspector]
    public Texture2D rawTexture;

    public void Init(EvidenceObject.Type type, Texture2D capturedImage)
    {
        this.type = type;
        image.texture = capturedImage;
        caption.text = EvidenceObject.Get(type).photoCaption;
        rawTexture = capturedImage;
    }
}
