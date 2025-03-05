using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextInteractable : MonoBehaviour, IInteractable
{
    [TextArea(1, 3)]
    public string text;
    public float time = 3f;
    public bool oneTimeOnly = false;

    public void OnClicked()
    {
        PopUp.Show(text, time);

        if (oneTimeOnly)
        {
            // Off ourselves
            Destroy(this);
            Interactor.RecalculateInteractionForCurrentObject();
        }
    }
}
