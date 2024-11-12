using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
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

    public void Interact(GameObject player)
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
