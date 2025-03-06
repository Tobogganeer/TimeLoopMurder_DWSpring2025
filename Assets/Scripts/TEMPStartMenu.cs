using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPStartMenu : MonoBehaviour
{
    static bool shown;

    // Only show menu once between scene reloads
    private void Start()
    {
        // Pause timer until we start
        FindObjectOfType<Loop>().timerPaused = true;
        if (shown)
            gameObject.SetActive(false);
        shown = true;
    }

    private void OnDisable()
    {
        Loop timer = FindObjectOfType<Loop>();
        if (timer)
            timer.timerPaused = false;
    }
}
