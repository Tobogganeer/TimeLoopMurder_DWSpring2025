using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableObjectOnClick : MonoBehaviour, IInteractable
{
    public GameObject targetObject;
    public bool targetActiveState = true;

    public void OnClicked() => targetObject.SetActive(targetActiveState);
}
