using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tobo.Audio;

public class VictoryScene : MonoBehaviour
{
    public Image blackScreen;
    public Image stopWatch;
    public float fadeInTime = 2f;

    IEnumerator Start()
    {
        blackScreen.color = Color.black; // Set alpha to one
        stopWatch.color = new Color(1, 1, 1, 0); // Transparent

        // TODO: Play sounds
        Sound.G_U_N.Play2D();
        yield return new WaitForSeconds(0.5f);
        Sound.Glass_Breaking.Play2D();
        yield return new WaitForSeconds(1f);
        Sound.Thud.Play2D();
        yield return new WaitForSeconds(1f);

        blackScreen.DOFade(0, fadeInTime);

        // Wait 1 extra second
        yield return new WaitForSeconds(fadeInTime + 1f);
        PopUp.Show("...Dad?", 2);

        // Fade back to black
        yield return new WaitForSeconds(1.5f);
        blackScreen.DOFade(1, fadeInTime);

        yield return new WaitForSeconds(fadeInTime + 1.5f);
        stopWatch.DOFade(1, 1f);

        // TODO: Play re-loop animation
    }
}
