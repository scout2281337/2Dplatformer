using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    [SerializeField] private WeaponSpawner[] _weaponSpawner = new WeaponSpawner[3];

    private void Start()
    {
        RerollItems();
    }

    public void Interact(GameObject player)
    {
        RerollItems();
    }

    private void RerollItems()
    {
        foreach (var item in _weaponSpawner)
        {
            item.SpawnWeapon();
        }
    }
}
