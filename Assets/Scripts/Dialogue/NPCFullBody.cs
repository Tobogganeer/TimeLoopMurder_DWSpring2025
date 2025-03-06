using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFullBody : MonoBehaviour, ICustomCursor, IInteractable, ICanHaveEvidenceDroppedOnMe
{
    public NPC.ID npc;

    // Show the question mark icon if we are holding evidence
    CursorType ICustomCursor.GetCursorType() =>
        DraggedPhotoGUI.CurrentlyHoldingEvidence ? CursorType.QuestionMark : CursorType.Speak;

    public void OnClicked()
    {
        DialogueGUI.Speak(npc, NPC.DialogueType.Greeting);
    }

    public void HandleEvidence(EvidenceObject.Type type)
    {
        DialogueGUI.Enable(npc);
        DialogueGUI.ClearChoices();
        DialogueGUI.AddBackChoice();
        DialogueGUI.AddChoice(type.GetQuestion(), () => DialogueGUI.Speak(npc, type));
        // TODO: Add choice and make sure we can't add it twice
        //DialogueGUI.Speak(npc, type);
    }
}