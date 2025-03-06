using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable, ICustomCursor
{
    public Direction direction;

    public void OnClicked()
    {
        Loop.TakeAction();

        if (direction == Direction.Left)
            CameraMovement.GoLeft();
        else
            CameraMovement.GoRight();
    }

    public CursorType GetCursorType() => direction == Direction.Left ? CursorType.LeftArrow : CursorType.RightArrow;


    public enum Direction
    {
        Left,
        Right
    }
}
