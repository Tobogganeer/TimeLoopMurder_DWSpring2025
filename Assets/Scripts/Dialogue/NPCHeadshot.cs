using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCHeadshot : MonoBehaviour, ICustomCursor
{
    // Show the question mark icon if we are holding evidence
    CursorType ICustomCursor.GetCursorType() =>
        DraggedPhotoGUI.CurrentlyHoldingEvidence ? CursorType.QuestionMark : CursorType.Default;
}
