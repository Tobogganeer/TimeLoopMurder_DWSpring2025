using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHeadshot : MonoBehaviour, ICustomCursor, ICanHaveEvidenceDroppedOnMe
{
    // Show the question mark icon if we are holding evidence
    CursorType ICustomCursor.GetCursorType() =>
        DraggedPhotoGUI.CurrentlyHoldingEvidence ? CursorType.QuestionMark : CursorType.Default;

    public void HandleEvidence(EvidenceObject.Type type)
    {
        // Let the dad handle evidence on his own (special cases for accusations)
        if (DialogueGUI.CurrentNPC.id == NPC.ID.Dad)
        {
            Dad.instance.HandleEvidence(type);
            return;
        }

        DialogueGUI.ClearChoices();
        DialogueGUI.AddBackChoice();
        DialogueGUI.AddChoice(type.GetQuestion(), () => DialogueGUI.Speak(DialogueGUI.CurrentNPC.id, type));
        //DialogueGUI.Speak(npc, type);
    }
}
