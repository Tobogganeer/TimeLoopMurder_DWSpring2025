using System.Collections;
using System.Collections.Generic;
using Tobo.Audio;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIHover : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public Mode mode;

    private static float lastHoverAudioTime;
    private static float lastClickAudioTime;
    private const float CLICK_MIN_DELAY = 0.06f;
    private const float HOVER_MIN_DELAY = 0.035f;

    public static void Hover()
    {
        if (lastHoverAudioTime - Time.time < -HOVER_MIN_DELAY)
        {
            Sound.SlotHover.Play2D();
            lastHoverAudioTime = Time.time;
        }
    }

    public static void Click()
    {
        if (lastClickAudioTime - Time.time < -CLICK_MIN_DELAY)
        {
            Sound.UIClick.Play2D();
            lastClickAudioTime = Time.time;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (mode != Mode.HoverOnly)
            Click();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (mode != Mode.ClickOnly)
            Hover();
    }

    public enum Mode
    {
        Both,
        HoverOnly,
        ClickOnly
    }
}
