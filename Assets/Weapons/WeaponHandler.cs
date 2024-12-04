using System;
using TMPro;
using UnityEngine;

public class WeaponHandler : Interactable
{
    public event Action OnWeaponTaken;

    [SerializeField] private TextMeshPro textMeshPro;

    public Weapon weapon { get; private set; }

    private void Start()
    {
        weapon = transform.parent.gameObject.GetComponent<Weapon>();
        SetWeaponHandler(weapon);
    }

    private void SetWeaponHandler(Weapon Weapon)
    {
        string name = Weapon.weaponStats.weaponName;
        textMeshPro.text = name;
    }

    private void AddWeaponToPlayer()
    {
        if (!PlayerManager.Instance.playerCombat.AddWeapon(transform.parent.gameObject))
            return;

        OnWeaponTaken?.Invoke();

        gameObject.SetActive(false);
    }

    public override void Interact()
    {
        base.Interact();
        AddWeaponToPlayer();
    }
}
