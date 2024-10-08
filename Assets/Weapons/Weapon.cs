using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public float fireRate;
    public float damage;
    public float projectileSpeed;
    public GameObject projectiletype;
    public GameObject weaponPickUp;
    public GameObject selfPrefab;
    protected float lastTimeShot;
    protected bool canFire = true;


    public virtual void WeaponAttack(Vector2 diraction, GameObject player)
    {
        Debug.Log("WeaponShoot");  
    }

    public void DropWeapon()
    {
        GameObject dropedWeapon = Instantiate(weaponPickUp, transform.position, Quaternion.identity);
        dropedWeapon.GetComponent<WeaponPickUp>().SetWeaponPickUp(selfPrefab);
        Destroy(gameObject);
    }
}
