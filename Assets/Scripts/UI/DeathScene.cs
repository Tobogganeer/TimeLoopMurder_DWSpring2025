using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tobo.Audio;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
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

        // Fade in stopwatch
        yield return new WaitForSeconds(fadeInTime + 1.5f);
        stopWatch.DOFade(1, 1f);

        // Play rewind audio, stacking and getting louder
        AudioManager.PlayAudio(Sound.Rewind.Override().SetVolume(0.2f));
        yield return new WaitForSeconds(0.8f);

        AudioManager.PlayAudio(Sound.Rewind.Override().SetVolume(0.5f));
        yield return new WaitForSeconds(0.8f);

        AudioManager.PlayAudio(Sound.Rewind.Override().SetVolume(0.8f));
        yield return new WaitForSeconds(0.8f);

        AudioManager.PlayAudio(Sound.Rewind.Override().SetVolume(1.0f));
        yield return new WaitForSeconds(0.8f);

        // Fade stopwatch back out
        stopWatch.DOFade(0f, 1f);
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("GameScene");
    }
}
