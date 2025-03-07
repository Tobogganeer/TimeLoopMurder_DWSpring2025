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

    int actionsLeft;
    int MinutesLeft => actionsLeft * minutesPerAction;

    public static int ActionsLeft => instance.actionsLeft;
    public static float PercentActionsLeft => instance.actionsLeft / (float)instance.startingActions;

    bool endingIsRunning;

    private void Start()
    {
        actionsLeft = startingActions;
    }

    public static void TakeAction()
    {
        instance.actionsLeft--;

        if (instance.actionsLeft <= 0)
            instance.LoopEnded(false);
    }

    public void LoopEnded(bool victory)
    {
        // Disable interaction. It will be re-enabled when the scene is reset
        // Note: This doesn't disable the pause menu and buttons (they don't use the Interaction system)
        Interactor.Enabled = false;

        if (!endingIsRunning)
            StartCoroutine(EndLoopCoroutine(victory));
    }

    IEnumerator EndLoopCoroutine(bool victory)
    {
        endingIsRunning = true;

        // TODO: Show cutscene before reload
        SceneManager.LoadScene("InteractionDemo");
    }

    public static void EndLoop(bool victory)
    {
        instance.LoopEnded(victory);
    }

    private void OnValidate()
    {
        startingMinutes = startingActions * minutesPerAction;
    }
}
