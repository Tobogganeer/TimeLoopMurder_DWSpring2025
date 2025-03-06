using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPStartMenu : MonoBehaviour
{
    static bool shown;

    // Only show menu once between scene reloads
    private void Start()
    {
        if (shown)
            gameObject.SetActive(false);
        shown = true;
    }
}
