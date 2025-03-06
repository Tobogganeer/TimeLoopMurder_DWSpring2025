using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogueButton : MonoBehaviour, ICustomCursor, IInteractable
{
    public void OnClicked()
    {
        if (!DialogueGUI.HasChoices)
            DialogueGUI.Disable();
    }

    CursorType ICustomCursor.GetCursorType() => DialogueGUI.HasChoices ? CursorType.Default : CursorType.DownArrow;
}
