using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Tobo.Attributes;
using UnityEngine.UI;
using DG.Tweening;

public class Loop : MonoBehaviour
{
    private static Loop instance;
    private void Awake()
    {
        instance = this;
    }

    public int startingActions = 12;

    [Space]
    public float ranOutOfActionsWaitTime = 2f;
    public float accusedSomeoneWaitTime = 5f;
    public float fadeTime = 1f;
    public Image blackScreen;

    int actionsLeft;

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
            instance.LoopEnded(false, false);
    }

    public void LoopEnded(bool victory, bool accusedSomeone)
    {
        // Disable interaction. It will be re-enabled when the scene is reset
        // Note: This doesn't disable the pause menu and buttons (they don't use the Interaction system)
        Interactor.Enabled = false;

        if (!endingIsRunning)
            StartCoroutine(EndLoopCoroutine(victory, accusedSomeone));
    }

    IEnumerator EndLoopCoroutine(bool victory, bool accusedSomeone)
    {
        endingIsRunning = true;

        // Wait longer if we accused someone (so we have time to read dad's comments)
        yield return new WaitForSeconds(accusedSomeone ? accusedSomeoneWaitTime : ranOutOfActionsWaitTime);

        blackScreen.gameObject.SetActive(true);
        blackScreen.DOFade(1f, fadeTime);

        // Wait on a black screen for a second
        yield return new WaitForSeconds(fadeTime + 1f);

        if (victory)
        {
            SceneManager.LoadScene("VictoryScene");
            // TODO: Play police sounds after scene loaded
        }
        else
        {
            // Lazy approach
            SceneManager.LoadScene("DeathScene");
        }

        //yield return null;
        // TODO: Show cutscene before reload
        //SceneManager.LoadScene("InteractionDemo");
    }

    public static void EndLoop(bool victory)
    {
        instance.LoopEnded(victory, true);
    }
}
