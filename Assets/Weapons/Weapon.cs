using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public string WeaponName;
    public float damage;
    public float fireRate;
    public float projectileSpeed;
    public float steamCost;
    public float heatGain;
    public GameObject projectileType;
    protected float currentSteam;
    protected float lastTimeShot;
    protected bool canShoot = true;

    [Header("Modules")]
    public GameObject weaponHandler;
    public GameObject spriteRenderer;

    //[Header("Reload")]
    //public int magCapacity;
    //public float reloadTime;
    //public int currentAmmo;
    //protected bool isReloading = false;


    public virtual bool WeaponAttack(Vector2 diraction, GameObject player)
    {
        if (Time.time > lastTimeShot + fireRate)
        {
            lastTimeShot = Time.time;

            return true;
        }
        else return false;

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

    public void ActivateWeapon()
    {
        spriteRenderer.SetActive(true);
    }

    public void DeactivateWeapon()
    {
        spriteRenderer.SetActive(false);
    }

    // Method to initiate the reloading process
    //protected void StartReloading()
    //{
    //    // Call the FinishReloading method after 'reloadTime' seconds
    //    Invoke(nameof(FinishReloading), reloadTime);
    //}

    // Method to complete the reload and reset ammo count
    //protected void FinishReloading()
    //{
    //    currentAmmo ++;
    //    if (currentAmmo < magCapacity)
    //    {
    //        StartReloading();
    //    }
    //}
}
