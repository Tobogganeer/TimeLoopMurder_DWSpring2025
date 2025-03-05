using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// You can override IInteractable in any Monobehaviour class
// The only thing required to be overriden is OnClicked - the other 3 are optional
// If this is on a UI object (i.e. a button, panel, etc), add a UIInteractable component
public class Interactable : MonoBehaviour, IInteractable
{
    public UnityEvent onClicked;

    [Space]
    public UnityEvent onCursorEnter;
    public UnityEvent onCursorStay;
    public UnityEvent onCursorExit;


    void IInteractable.OnClicked() => onClicked?.Invoke();

    void IInteractable.OnCursorEnter() => onCursorEnter?.Invoke();
    void IInteractable.OnCursorStay() => onCursorStay?.Invoke();
    void IInteractable.OnCursorExit() => onCursorExit?.Invoke();
}

/// <summary>
/// Makes this object recieve callbacks about clicks and cursor events (enter, exit, hover)
/// </summary>
public interface IInteractable
{
    /// <summary>
    /// Called when this object is left clicked
    /// </summary>
    public void OnClicked();

    /// <summary>
    /// Called when the cursor hovers over this object
    /// </summary>
    public void OnCursorEnter() { }
    /// <summary>
    /// Called every frame the cursor is over this object
    /// </summary>
    public void OnCursorStay() { }
    /// <summary>
    /// Called when the cursor stops hovering over this object
    /// </summary>
    public void OnCursorExit() { }
}
