using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tobo.Audio;

public class GameSceneIntro : MonoBehaviour
{
    public Image blackScreen;
    public float fadeInTime = 1f;

    IEnumerator Start()
    {
        blackScreen.color = Color.black; // Set alpha to one
        blackScreen.DOFade(0, fadeInTime);

        yield return new WaitForSeconds(fadeInTime);
        blackScreen.gameObject.SetActive(false); // Make sure we can interact through it
        PopUp.Show("1 hour ago...", 2);
    }
}
