using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopStopwatch : MonoBehaviour
{
    public Transform hand;
    public Image backgroundFill;

    private void Update()
    {
        // 0 when we have all actions left, 1 when we are finished
        float targetFac = 1f - Loop.PercentActionsLeft;

        hand.rotation = Quaternion.Euler(0, 0, targetFac * -360f);
        backgroundFill.fillAmount = targetFac;
    }
}
