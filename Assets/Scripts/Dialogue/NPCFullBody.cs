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
        DialogueGUI.Enable(npc);
    }

    public void HandleEvidence(EvidenceObject.Type type)
    {
        // TODO: Add choice and make sure we can't add it twice
        //DialogueGUI.Speak(npc, type);
    }
}