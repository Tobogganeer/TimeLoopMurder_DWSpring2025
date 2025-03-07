using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Safe : MonoBehaviour, IInteractable
{
    public GameObject puzzleUI;

    public void OnClicked()
    {
        puzzleUI.SetActive(true);
    }
}
