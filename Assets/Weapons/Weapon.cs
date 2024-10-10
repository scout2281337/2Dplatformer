using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public string WeaponName;
    public float fireRate;
    public float damage;
    //public int magCapacity;
    //public float reloadCooldown;
    public float projectileSpeed;
    public GameObject projectileType;
    public GameObject weaponHandler;
    protected float lastTimeShot;
    protected bool canFire = true;


    public virtual void WeaponAttack(Vector2 diraction, GameObject player)
    {
        Debug.Log("WeaponShoot");  
    }
    public void DropWeapon()
    {
        transform.parent = null; 
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        weaponHandler.SetActive(true);
    }

    public void WeaponPickUp(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCombat>().AddWeapon(gameObject))
        {
            weaponHandler.SetActive(false);
        }
    }
}
