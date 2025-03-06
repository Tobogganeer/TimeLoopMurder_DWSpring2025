using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using Tobo.Util;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private static CameraMovement instance;
    private void Awake()
    {
        instance = this;
    }

    public float xOffset = 2.8f;
    public CameraPosition current;

    private void Start()
    {
        SetNewPosition(current);
    }

    public void SetNewPosition(CameraPosition newPosition)
    {
        current = newPosition;
        transform.position = transform.position.WithX(current.transform.position.x + xOffset);
    }

    public static void GoLeft()
    {
        instance.SetNewPosition(instance.current.left);
        Sound.Move.Play2D();
    }

    public static void GoRight()
    {
        instance.SetNewPosition(instance.current.right);
        Sound.Move.Play2D();
    }
}
