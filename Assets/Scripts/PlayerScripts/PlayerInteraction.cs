using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    private Interactable interactable;

    public event Action<Interactable> OnFocusInteractable;
    public event Action OnUnFocusInteractable;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void Interact()
    {
        if (interactable == null)
            return;
        
        interactable.Interact();
    }

    public bool FocusInteractable(Interactable newInteractable)
    {
        if (interactable != null)
            return false;

        interactable = newInteractable;

        OnFocusInteractable?.Invoke(interactable);
        return true;
    }

    public void UnFocuesInteractable()
    {
        if (interactable == null)
            return;

        OnUnFocusInteractable?.Invoke();

        interactable = null;
    }
}
