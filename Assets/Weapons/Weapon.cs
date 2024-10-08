using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public float fireRate;
    public float damage;
    public float projectileSpeed;
    public GameObject projectiletype;
    protected float lastTimeShot;
    protected bool canFire = true;


    public virtual void WeaponAttack(Vector2 diraction, GameObject player)
    {
        Debug.Log("WeaponShoot");  
    }
}
