using System.Collections;
using System.Collections.Generic;
using Tobo.Util;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Mode mode;
    //public Vector3 rotationOffset;
    public bool lockToVerticalAxis = true;
    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    private void Update()
    {
        Vector3 lookDir = transform.position.Dir(cam.position);
        if (mode == Mode.MatchCameraForwardVector)
            lookDir = -cam.forward;

        if (lockToVerticalAxis)
            lookDir.y = 0f;

        transform.forward = lookDir.normalized;

        //transform.Rotate(rotationOffset, Space.Self);
    }

    public enum Mode
    {
        FaceCamera,
        MatchCameraForwardVector
    }
}
