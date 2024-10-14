using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour, IInteractable
{
    public int amountOfStats;

    public GameObject[] WeaponType = new GameObject[4];


    public void Interact()
    {
        SpawnWeapon();
    }

    private void SpawnWeapon()
    {
        GameObject weapon = WeaponType[Random.Range(0, WeaponType.Length)];
        GameObject spawnedWeapon = Instantiate(weapon);
        Weapon weaponComponent = spawnedWeapon.GetComponent<Weapon>();

        int rang = 0;
        int pontsAmount = 4 + rang * 2;

        for (int i = 0; i < pontsAmount; i++)
        {
            switch (Random.Range(1, amountOfStats))
            {
                case 0:
                    weaponComponent.damage *= 2;
                    Debug.Log("damage: " + weaponComponent.damage);
                    break;
                case 1:
                    weaponComponent.fireRate /= 2;
                    Debug.Log("firerate: " + weaponComponent.fireRate);
                    break;
                case 2:
                    weaponComponent.magCapacity *= 2;
                    Debug.Log("magcapacity: " + weaponComponent.magCapacity);
                    break;
                case 3:
                    weaponComponent.reloadTime /= 2;
                    Debug.Log("reloadtime: " + weaponComponent.reloadTime);
                    break;
                case 4:
                    Debug.Log("adadaa");
                    break;
            }
        }

        spawnedWeapon.transform.position = transform.position;
    }
}

