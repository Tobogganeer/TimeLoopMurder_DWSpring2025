using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float xOffset = 2.8f;
    public CameraPosition current;
    public GameObject leftButton;
    public GameObject rightButton;

    private void Start()
    {
        SetNewPosition(current);
    }

    public void SetNewPosition(CameraPosition newPosition)
    {
        current = newPosition;
        transform.position = transform.position.WithX(current.transform.position.x + xOffset);
        leftButton.SetActive(current.left);
        rightButton.SetActive(current.right);
    }

    public void GoLeft()
    {
        SetNewPosition(current.left);
    }

    public void GoRight()
    {
        SetNewPosition(current.right);
    }
}
