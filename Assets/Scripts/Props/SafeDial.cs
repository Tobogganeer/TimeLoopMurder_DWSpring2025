using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Tobo.Audio;

public class SafeDial : MonoBehaviour, IInteractable
{
    public GameObject safeUIToDisableWhenCorrect;
    public GameObject closedSafe;
    public GameObject openSafe;

    public TMP_Text leftNumberText;
    public TMP_Text rightNumberText;
    public int pixelThreshold = 20;
    public int maxNumber = 39;
    public float dialRotation = 125f;

    [Space]
    public string correctCombo = "1635";

    bool dragging;

    int leftNumber;
    int rightNumber;

    float mouseDelta;
    Vector2 oldMousePos;

    public string CurrentCombo => leftNumber.ToString().PadLeft(2, '0') + rightNumber.ToString().PadLeft(2, '0');

    public void OnClicked()
    {
        dragging = true;
        mouseDelta = 0f;
        oldMousePos = Mouse.current.position.value;
    }

    private void Update()
    {
        if (!dragging || CurrentCombo == correctCombo)
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

        if (numTicks > 0)
            Sound.SlotHover.Play2D();

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

        CheckForCorrectCombo();
    }

    void CheckForCorrectCombo()
    {
        if (CurrentCombo == correctCombo)
        {
            // Stop input
            dragging = false;
            Sound.UIClick2.Play2D();
            Invoke(nameof(ExitSafe), 1f); // Wait 1 second
        }
    }

    // Called after a delay using Invoke
    void ExitSafe()
    {
        safeUIToDisableWhenCorrect.SetActive(false);
        closedSafe.SetActive(false);
        openSafe.SetActive(true);
        Sound.Safe.Play2D();
    }
}
