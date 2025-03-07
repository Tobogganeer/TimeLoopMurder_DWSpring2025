using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    private static Interactor instance;
    private void Awake()
    {
        instance = this;
    }

    public CursorTypes cursors;

    public bool useInteractHandWhenNoCustomCursorProvided = true;

    Camera cam;
    IInteractable[] current; // List of all interactables on the hovered object
    ICustomCursor currentCursor; // If the hovered object wants a custom cursor, it'll be stored here

    GameObject currentObject; // The object we are currently hovered over

    bool isEnabled = true;

    public static GameObject CurrentObject => instance.currentObject;
    public static bool Enabled { get => instance.isEnabled; set => instance.isEnabled = value; }

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (!isEnabled)
        {
            // Set default cursor
            cursors.SetCursorType(CursorType.Default);

            // Remove all current interactables
            NoInteractableFound();
            currentObject = null;
            currentCursor = null;
            return;
        }

        CastRay();
        // Set the current cursor type if we are hovering over something
        if (currentCursor == null)
            cursors.SetCursorType(GetCursorTypeWithNoCustomCursorFound());
        else
            cursors.SetCursorType(currentCursor.GetCursorType());
    }

    CursorType GetCursorTypeWithNoCustomCursorFound()
    {
        // If the target object has no custom cursor, should we show the interact hand by default?
        if (useInteractHandWhenNoCustomCursorProvided)
            return current?.Length > 0 ? CursorType.InteractHand : CursorType.Default;
        else return CursorType.Default;
    }

    void CastRay()
    {
        GameObject hitObject = null;

        // Check if we are hovering over UI
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            // Returns null
            //hitObject = EventSystem.current.currentSelectedGameObject;
            if (UIInteractable.CurrentlyHovered != null)
                hitObject = UIInteractable.CurrentlyHovered.gameObject;
        }
        else
        {
            // Otherwise cast a ray into the scene
            Ray ray = cam.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(ray, out RaycastHit hit))
                hitObject = hit.collider.gameObject;
        }

        // Check for object
        if (hitObject != null)
        {
            // We hit the same object again - no need to check further
            if (currentObject == hitObject)
            {
                CallStayForAll();
                // Check if we are trying to interact
                if (Mouse.current.leftButton.wasPressedThisFrame)
                {
                    // Do it
                    CallOnClickedForAll();
                }
            }
            else
            {
                currentObject = hitObject;

                // Call OnCursorExit for all of the current (old) interactables
                CallExitForAll();

                // Check if the new object is interactable
                if (hitObject.TryGetComponent<IInteractable>(out _))
                {
                    // Fill our list with the interactables on the new object
                    current = hitObject.GetComponents<IInteractable>();

                    CallEnterForAll();

                    // Check if we are trying to interact
                    if (Mouse.current.leftButton.wasPressedThisFrame)
                    {
                        // Do it
                        CallOnClickedForAll();
                    }
                }
                else NoInteractableFound();
            }

            // Check if has a custom cursor
            if (!hitObject.TryGetComponent(out currentCursor))
                currentCursor = null;
        }
        else
        {
            // Hit nothing
            NoInteractableFound();
            currentObject = null;
            currentCursor = null;
        }
    }

    void NoInteractableFound()
    {
        // Call OnCursorExit on all current interactables
        CallExitForAll();
        current = null;
    }

    /// <summary>
    /// Call when you remove a component at runtime (e.g. single time TextInteractable)
    /// </summary>
    public static void RecalculateInteractionForCurrentObject()
    {
        if (instance.currentObject == null)
            instance.current = null;
        else
            instance.current = instance.currentObject.GetComponents<IInteractable>();
    }

    #region Shorthands to call functions on all current interactables
    void CallFunctionOnAll(Action<IInteractable> action)
    {
        if (current == null)
            return;

        for (int i = 0; i < current.Length; i++)
            action(current[i]);
    }

    void CallOnClickedForAll() => CallFunctionOnAll((inter) => inter.OnClicked());
    void CallEnterForAll() => CallFunctionOnAll((inter) => inter.OnCursorEnter());
    void CallStayForAll() => CallFunctionOnAll((inter) => inter.OnCursorStay());
    void CallExitForAll() => CallFunctionOnAll((inter) => inter.OnCursorExit());
    #endregion
}
