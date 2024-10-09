using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject weapon;

    private void Start()
    {
        SetWeaponPickUp(transform.GetChild(0).gameObject);
    }

    public void SetWeaponPickUp(GameObject newWeapon)
    {
        weapon = newWeapon;
        weapon.transform.parent = transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCombat>().AddWeapon(weapon))
        {
            Destroy(gameObject);
        }
    }
}
