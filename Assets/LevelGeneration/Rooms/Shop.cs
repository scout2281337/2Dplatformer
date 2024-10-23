using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Shop : MonoBehaviour
{
    [SerializeField] private GameObject[] shopItems = new GameObject[3];

    private void Start()
    {
        foreach (GameObject item in shopItems)
        {
            item.GetComponent<WeaponSpawner>().SpawnWeapon();
        }
    }
}

public class ShopItem
{
    private GameObject _item;

    public void SpawnNewWeapon()
    {

    }

}
