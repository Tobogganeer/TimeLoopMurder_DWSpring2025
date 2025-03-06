using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopStopwatch : MonoBehaviour
{
    public Transform hand;
    public Image backgroundFill;

    float bgFill;
    int oldActionsLeft;

    const float BGFillSpeed = 3f;
    const float ScaleSpeed = 3f;
    const float PopScaleSize = 0.1f;

    private void Update()
    {
        bool justUsedAnAction = oldActionsLeft != Loop.ActionsLeft;
        oldActionsLeft = Loop.ActionsLeft;

        if (justUsedAnAction)
            transform.localScale = Vector3.one * (PopScaleSize + transform.localScale.x);

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * ScaleSpeed);

        // 0 when we have all actions left, 1 when we are finished
        float targetFac = 1f - Loop.PercentActionsLeft;

        hand.rotation = Quaternion.Euler(0, 0, targetFac * -360f);

        bgFill = Mathf.Lerp(bgFill, targetFac, Time.deltaTime * BGFillSpeed);
        backgroundFill.fillAmount = bgFill;
    }
}
