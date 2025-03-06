using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class SafeDial : MonoBehaviour, IInteractable
{
    public TMP_Text leftNumberText;
    public TMP_Text rightNumberText;
    public int pixelThreshold = 20;
    public int maxNumber = 39;
    public float dialRotation = 125f;

    bool dragging;

    int leftNumber;
    int rightNumber;

    float mouseDelta;
    Vector2 oldMousePos;

    public void OnClicked()
    {
        dragging = true;
        mouseDelta = 0f;
        oldMousePos = Mouse.current.position.value;
    }

    private void Update()
    {
        if (!dragging)
            return;

        // Get how many pixels we have moved
        Vector2 delta = Mouse.current.position.value - oldMousePos;
        oldMousePos = Mouse.current.position.value;
        mouseDelta += delta.x;

        transform.Rotate(0, 0, -delta.x * dialRotation * Time.deltaTime);

        // Get number of times we have crossed threshold
        //int numTicks = (int)Mathf.Abs(mouseDelta) % pixelThreshold;
        int numTicks = Mathf.Abs(mouseDelta) > pixelThreshold ? 1 : 0;
        float sign = Mathf.Sign(mouseDelta);
        if (sign > 0) // Dragging right
            leftNumber += numTicks;
        else
            rightNumber += numTicks;

        // Reset mouseDelta
        mouseDelta -= (numTicks * pixelThreshold * sign);

        // Clamp/wrap around
        if (leftNumber > maxNumber)
            leftNumber -= maxNumber;
        if (rightNumber > maxNumber)
            rightNumber -= maxNumber;

        // See if we aren't dragging anymore
        if (dragging && Mouse.current.leftButton.wasReleasedThisFrame)
        {
            dragging = false;
        }

        leftNumberText.text = leftNumber.ToString().PadLeft(2, '0');
        rightNumberText.text = rightNumber.ToString().PadLeft(2, '0');
    }
}
