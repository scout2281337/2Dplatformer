using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    private bool isFocused = false;

    public virtual void Interact()
    {
        PlayerManager.Instance.playerInteraction.UnFocuesInteractable();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != PlayerManager.Instance.player)
            return;

        if (!PlayerManager.Instance.playerInteraction.FocusInteractable(this))
            return;

        isFocused = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject != PlayerManager.Instance.player)
            return;

        if (!isFocused)
            return;

        PlayerManager.Instance.playerInteraction.UnFocuesInteractable();
        isFocused = false;
    }
}
