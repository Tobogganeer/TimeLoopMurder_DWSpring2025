using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dad : MonoBehaviour, ICustomCursor, IInteractable, ICanHaveEvidenceDroppedOnMe
{
    // Show the question mark icon if we are holding evidence
    CursorType ICustomCursor.GetCursorType() =>
        DraggedPhotoGUI.CurrentlyHoldingEvidence ? CursorType.QuestionMark : CursorType.Speak;

    public void OnClicked()
    {
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
        DialogueGUI.Speak(NPC.ID.Dad, "What's that, kiddo?", false);
        DialogueGUI.AddChoice("Warn him", () => WarnDad(), false);
        DialogueGUI.AddChoice("\"Goodbye.\"", () => DialogueGUI.Speak(NPC.ID.Dad, "Oh, ok.", false), false);
    }

    void WarnDad()
    {
        DialogueGUI.AddBackChoice();
        DialogueGUI.AddChoice("Accuse Lysander", () => Accuse(NPC.ID.Investor));
        DialogueGUI.AddChoice("Accuse Kensington", () => Accuse(NPC.ID.Mistress));
        DialogueGUI.AddChoice("Accuse Yorick", () => Accuse(NPC.ID.Butler));
        DialogueGUI.AddChoice("Accuse Zephyer", () => Accuse(NPC.ID.Mom));
    }

    void Accuse(NPC.ID npc)
    {

    }
}