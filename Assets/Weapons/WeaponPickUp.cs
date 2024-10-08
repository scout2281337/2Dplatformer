using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject weapon;
    public SpriteRenderer spriteRenderer;

    private void Start()
    {
        SetWeaponPickUp(weapon);
    }

    public void SetWeaponPickUp(GameObject newWeapon)
    {
        if (newWeapon != null)
        {
            weapon = newWeapon;
            spriteRenderer.sprite = weapon.GetComponent<SpriteRenderer>().sprite;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCombat>().AddWeapon(weapon))
        {
            Destroy(gameObject);
        }
        
    }
}
