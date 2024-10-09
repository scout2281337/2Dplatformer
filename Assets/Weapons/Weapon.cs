using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public abstract class Weapon : MonoBehaviour
{
    [Header("Weapon")]
    public float fireRate;
    public float damage;
    public float projectileSpeed;
    public GameObject projectileType;
    protected float lastTimeShot;
    protected bool canFire = true;


    public virtual void WeaponAttack(Vector2 diraction, GameObject player)
    {
        Debug.Log("WeaponShoot");  
    }
    public void DropWeapon()
    {
        transform.parent = null; 
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerCombat>().AddWeapon(gameObject))
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
