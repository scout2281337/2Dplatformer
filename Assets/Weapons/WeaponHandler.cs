using System;
using TMPro;
using UnityEngine;

public class WeaponHandler : MonoBehaviour, IInteractable
{
    public TextMeshPro textMeshPro;

    public event Action OnWeaponTaken;

    private void Start()
    {
        SetWeaponHandler(transform.parent.gameObject);
    }

    private void SetWeaponHandler(GameObject Weapon)
    {
        string name = Weapon.GetComponent<Weapon>()?.weaponStats.WeaponName;
        textMeshPro.text = name;
    }

    private void AddWeaponToPlayer(GameObject player)
    {
        if (player.GetComponent<PlayerCombat>().AddWeapon(transform.parent.gameObject))
        {
            gameObject.SetActive(false);

            OnWeaponTaken?.Invoke();
        }
    }

    public void Interact(GameObject player)
    {
        AddWeaponToPlayer(player);
    }
}
