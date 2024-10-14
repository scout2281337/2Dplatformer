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
    public int damage;
    public float fireRate;
    public float projectileSpeed;
    public GameObject projectileType;
    protected float lastTimeShot;
    protected bool canShoot = true;

    [Header("Reload")]
    public int magCapacity;
    public float reloadTime;
    public int currentAmmo;
    protected bool isReloading = false;

    [Header("Modules")]
    public GameObject weaponHandler;
    public GameObject spriteRenderer;

    protected void Start()
    {
        // Initialize current ammo to the magazine capacity
        currentAmmo = magCapacity;
    }

    public virtual void WeaponAttack(Vector2 diraction, GameObject player)
    {
        if (currentAmmo > 0 && Time.time > lastTimeShot + fireRate)
        {

            if (magCapacity == currentAmmo)
            {
                StartReloading();
            }

            // Deduct one ammo per shot
            currentAmmo--;

            // Gets time to compare when shooting to preserve firerate
            lastTimeShot = Time.time;

            canShoot = true;
        }
        else
        {
            canShoot = false;
        }
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

    // Method to initiate the reloading process
    protected void StartReloading()
    {
        // Call the FinishReloading method after 'reloadTime' seconds
        Invoke(nameof(FinishReloading), reloadTime);
    }

    // Method to complete the reload and reset ammo count
    protected void FinishReloading()
    {
        currentAmmo ++;
        if (currentAmmo < magCapacity)
        {
            StartReloading();
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
}
