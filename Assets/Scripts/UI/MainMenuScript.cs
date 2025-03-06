using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public bool isStartMenu = false;

    public void SceneLoad()
    {
        if (isStartMenu) { SceneManager.LoadScene("InteractionDemo"); }
        else { SceneManager.LoadScene("Main Menu"); }
    }
    
}
