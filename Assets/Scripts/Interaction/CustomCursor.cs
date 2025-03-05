using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is a basic example of how to use the ICustomCursor interface
// You could change the cursor throughout gameplay
// e.g. return CursorType.Lock when locked and CursorType.InteractHand otherwise
// If this is on a UI object (i.e. a button, panel, etc), add a UIInteractable component
public class CustomCursor : MonoBehaviour, ICustomCursor
{
    public CursorType cursorType;

    public CursorType GetCursorType() => cursorType;
}

public interface ICustomCursor
{
    public CursorType GetCursorType();
}