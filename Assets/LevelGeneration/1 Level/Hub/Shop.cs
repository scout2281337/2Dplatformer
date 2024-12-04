using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : Interactable
{
    [SerializeField] private float minModifier;
    [SerializeField] private float maxModifier;
    [SerializeField] private ComponentsList_SO componentsList;
    [SerializeField] private WeaponSpawner[] _weaponSpawners = new WeaponSpawner[3];

    private void Start()
    {
        SetAllWeaponSpawners();
        RerollItems();
    }

    public override void Interact()
    {
        RerollItems();
    }

    private void RerollItems()
    {
        foreach (var spawner in _weaponSpawners)
        {
            spawner.SpawnWeapon();
        }
    }

    private void SetAllWeaponSpawners()
    {
        foreach (var spawner in _weaponSpawners)
        {
            spawner.SetWeaponSpawner(minModifier, maxModifier, componentsList);
        }
    }
}
