using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    static PauseMenu instance;
    private void Awake()
    {
        instance = this;
    }

    public GameObject pauseMenu;

    // TODO: Reset to original state (sub-menus closed) when opening pause menu

    public static bool IsPaused => instance.pauseMenu.activeSelf;

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
            TogglePauseMenu();
    }

    public void TogglePauseMenu()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
    }

    public void ExitToMainMenu()
    {
        // TODO: Exit to main menu once implemented
        SceneManager.LoadScene("Main Menu");
//#if UNITY_EDITOR
//        UnityEditor.EditorApplication.isPlaying = false;
//#endif
    }
}
