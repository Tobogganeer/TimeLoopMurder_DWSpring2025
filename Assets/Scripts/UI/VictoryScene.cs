using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Tobo.Audio;
using UnityEngine.SceneManagement;

public class VictoryScene : MonoBehaviour
{
    public Image blackScreen;
    public float fadeInTime = 2f;

    void Start()
    {
        // TODO: Play sirens/paper sound?

        blackScreen.color = Color.black; // Set alpha to one
        blackScreen.DOFade(0, fadeInTime);

        Sound.PhotoDrop.Play2D(); // Emulate paper sound for now
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
