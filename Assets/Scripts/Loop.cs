using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Loop : MonoBehaviour
{
    public float targetLimit = 60.0f;
    public bool timerPaused = false;

    public TMP_Text timer;

    public void Update()
    {
        timer.SetText("Loop reset: " + Mathf.RoundToInt(targetLimit).ToString());

        if (!timerPaused)
        {
            targetLimit -= Time.deltaTime;

            if (targetLimit <= 0.0f)
            {
                timerEnded();
            }
        }

    }

    void timerEnded()
    {
        SceneManager.LoadScene("InteractionDemo");
        //Reload scene here
    }
}
