using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    public float explosionRadius;
    public float explosionForce;


    public override bool WeaponAttack(Vector2 direction, GameObject player)
    {
        if (!base.WeaponAttack(direction, player)) return false;

        //shot
        GameObject bullet = Instantiate(projectileType, transform.position, Quaternion.identity); //Spawns bullet
        bullet.GetComponent<Grenade>().SetGrenade(projectileSpeed, direction, damage, explosionRadius, explosionForce); //Sets bullets mandatory vars
        
        return true;
    }
}
