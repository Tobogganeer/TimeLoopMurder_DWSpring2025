using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHeadshot : MonoBehaviour, ICustomCursor, ICanHaveEvidenceDroppedOnMe
{
    public NPC.ID npc;
    // Show the question mark icon if we are holding evidence
    CursorType ICustomCursor.GetCursorType() =>
        DraggedPhotoGUI.CurrentlyHoldingEvidence ? CursorType.QuestionMark : CursorType.Default;

    public void HandleEvidence(EvidenceObject.Type type)
    {
        if (!DialogueGUI.HasEvidenceButton(type))
            DialogueGUI.AddChoice()
        //DialogueGUI.Speak(npc, type);
    }
}
