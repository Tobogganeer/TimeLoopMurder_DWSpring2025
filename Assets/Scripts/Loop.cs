using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Tobo.Attributes;

public class Loop : MonoBehaviour
{
    private static Loop instance;
    private void Awake()
    {
        instance = this;
    }

    public int startingActions = 12;
    public int minutesPerAction = 5;

    [ReadOnly, SerializeField] // Only used to show in inspector
    private int startingMinutes;

    public TMP_Text timerText;

    int actionsLeft;
    int MinutesLeft => actionsLeft * minutesPerAction;

    public static int ActionsLeft => instance.actionsLeft;

    private void Start()
    {
        actionsLeft = startingActions;
        UpdateUI();
    }
    public void UpdateUI()
    {
        timerText.SetText("Minutes remaining in loop: " + MinutesLeft);
    }

    public static void TakeAction()
    {
        instance.actionsLeft--;
        instance.UpdateUI();

        if (instance.actionsLeft <= 0)
            instance.LoopEnded();
    }

    void LoopEnded()
    {
        // Disable interaction. It will be re-enabled when the scene is reset
        // Note: This doesn't disable the pause menu and buttons (they don't use the Interaction system)
        Interactor.Enabled = false;

        // TODO: Show cutscene before reload
        SceneManager.LoadScene("InteractionDemo");
    }

    private void OnValidate()
    {
        startingMinutes = startingActions * minutesPerAction;
    }
}
