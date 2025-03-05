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

    public void Init(EvidenceObject.Type type)
    {
        this.type = type;
        image.texture = EvidenceObject.Get(type).capturedImage;
        caption.text = EvidenceObject.Get(type).photoCaption;
    }
}
