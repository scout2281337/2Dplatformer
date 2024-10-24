using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using UnityEngine;

public class Shop : MonoBehaviour, IInteractable
{
    [SerializeField] private WeaponSpawner[] _weaponSpawner = new WeaponSpawner[3];
    private ShopItem[] _shopItems = new ShopItem[3];

    public void Interact(GameObject player)
    {
        RerollItems();
    }

    private void RerollItems()
    {
        foreach (var item in _shopItems)
        {
            item.SpawnNewWeapon();
        }
    }
    private void Start()
    {
        for (int i = 0;  i < _weaponSpawner.Length; i++)
        {
            _shopItems[i] = new ShopItem();
            _shopItems[i].SetShopItem(_weaponSpawner[i]);
        }
    }
}

public class ShopItem
{
    private WeaponSpawner _item;

    public void SetShopItem(WeaponSpawner item)
    {
        _item = item;
        SpawnNewWeapon();
    } 
    public void SpawnNewWeapon()
    {
        _item.SpawnWeapon();
    }
}
