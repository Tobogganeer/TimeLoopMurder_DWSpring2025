using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class FadeToBlack : MonoBehaviour
{
    public Image BlackScreen;
    public float delayTime = 4f;
    public float fadeTime = 2f;
    public void fadeOut()
    {
        BlackScreen.DOFade(1, fadeTime).onComplete += () => fadeIn();
    }

    public void fadeIn()
    {
        BlackScreen.DOFade(0, fadeTime).SetDelay(fadeTime);
    }
}
