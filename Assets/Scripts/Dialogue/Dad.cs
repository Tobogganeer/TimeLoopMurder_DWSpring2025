using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;

public class Dad : MonoBehaviour, ICustomCursor, IInteractable, ICanHaveEvidenceDroppedOnMe
{
    public static Dad instance;
    private void Awake()
    {
        instance = this;
    }

    enum State
    {
        Speaking,
        WaitingForMotive,
        WaitingForMeans,
    }

    PooledAudioSource suspenseTheme;

    State state;
    NPC.ID accusedNPC;
    EvidenceObject.Type guessedMotive;
    EvidenceObject.Type guessedMeans;

    // Show the question mark icon if we are holding evidence
    CursorType ICustomCursor.GetCursorType() =>
        DraggedPhotoGUI.CurrentlyHoldingEvidence ? CursorType.QuestionMark : CursorType.Speak;

    public void OnClicked()
    {
        state = State.Speaking;
        suspenseTheme = null;

        DialogueGUI.Speak(NPC.ID.Dad, NPC.DialogueType.Greeting, false);
        // Goodbye
        DialogueGUI.AddChoice("\"Goodbye.\"", () => DialogueGUI.Speak(NPC.ID.Dad, NPC.DialogueType.Farewell, false), false);
        // Dinner :)
        DialogueGUI.AddChoice("\"Can we spend some time together after dinner?\"",
            () => DialogueGUI.Speak(NPC.ID.Dad, "Sure we can! Let's go to the backyard and play baseball when we're all fed, yeah?", false), false);
        DialogueGUI.AddChoice("Warn him", () => WarnDad(), false);
    }

    public void HandleEvidence(EvidenceObject.Type type)
    {
        switch (state)
        {
            // We are just talking - let the player start accusing
            case State.Speaking:
                DialogueGUI.Speak(NPC.ID.Dad, "What's that, kiddo?", false);
                DialogueGUI.AddChoice("Warn him", () => WarnDad(), false);
                DialogueGUI.AddChoice("\"Goodbye.\"", () => DialogueGUI.Speak(NPC.ID.Dad, "Oh, ok.", false), false);
                break;
            case State.WaitingForMotive:
                HandleMotive(type);
                break;
            case State.WaitingForMeans:
                HandleMeans(type);
                break;
            default:
                break;
        }
        
    }

    void WarnDad()
    {
        suspenseTheme = Sound.Suspense_1_Loop.Play2D();
        DialogueGUI.ClearChoices();
        //DialogueGUI.AddBackChoice();
        AddNevermindChoice();
        DialogueGUI.AddChoice("Accuse Lysander", () => Accuse(NPC.ID.Investor), false);
        DialogueGUI.AddChoice("Accuse Kensington", () => Accuse(NPC.ID.Mistress), false);
        DialogueGUI.AddChoice("Accuse Yorick", () => Accuse(NPC.ID.Butler), false);
        DialogueGUI.AddChoice("Accuse Zephyer", () => Accuse(NPC.ID.Mom), false);
    }

    void Accuse(NPC.ID npc)
    {
        state = State.WaitingForMotive;
        accusedNPC = npc;

        DialogueGUI.Speak(NPC.ID.Dad, npc.GetName() + "? Why? [drag evidence]", false);
        AddNevermindChoice();
        // Player then drags in evidence
    }

    void HandleMotive(EvidenceObject.Type evidence)
    {
        state = State.WaitingForMeans;
        guessedMotive = evidence;
        DialogueGUI.Speak(NPC.ID.Dad, evidence.GetDadMotiveComment() + " What are they going to do? [drag evidence]", false);
        AddNevermindChoice();
    }

    void HandleMeans(EvidenceObject.Type evidence)
    {
        guessedMeans = evidence;
        DialogueGUI.Speak(NPC.ID.Dad, evidence.GetDadMeansComment() + " Let me look into this.", false);
        Loop.EndLoop(accusedNPC == NPC.ID.Butler && guessedMotive ==
            EvidenceObject.Type.ChangedWill && guessedMeans == EvidenceObject.Type.GunCabinet); // End loop - see what happens
    }

    void AddNevermindChoice()
    {
        DialogueGUI.AddChoice("\"..Nevermind\"", () =>
        {
            DialogueGUI.Speak(NPC.ID.Dad, "Oh, ok.", false);
            if (suspenseTheme != null)
                suspenseTheme.gameObject.SetActive(false);
        }
        , false);
    }
}