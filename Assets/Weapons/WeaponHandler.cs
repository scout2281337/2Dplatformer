using System;
using TMPro;
using UnityEngine;

public class WeaponHandler : MonoBehaviour, IInteractable
{
    public TextMeshPro textMeshPro;
    public Action OnAddWeapon;

    private void OnEnable()
    {
        SetWeaponHandler(transform.parent.gameObject);
    }

    public void SetWeaponHandler(GameObject Weapon)
    {
        string name = Weapon.GetComponent<Weapon>().WeaponName;
        textMeshPro.text = name;
    }

    private void AddWeaponToPlayer(GameObject player)
    {
        if (player.GetComponent<PlayerCombat>().AddWeapon(transform.parent.gameObject))
        {
            gameObject.SetActive(false);

            OnAddWeapon?.Invoke();
        }
    }

    void IInteractable.Interact(GameObject player)
    {
        AddWeaponToPlayer(player);
    }
}
