using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    public float explosionRadius;
    public float explosionForce;


    public override void WeaponAttack(Vector2 diraction, GameObject player)
    {
        base.WeaponAttack(diraction, player);

        if (canShoot)
        {
            //shot
            GameObject bullet = Instantiate(projectileType, transform.position, Quaternion.identity); //Spawns bullet
            bullet.GetComponent<Grenade>().SetGrenade(projectileSpeed, diraction, damage, explosionRadius, explosionForce); //Sets bullets mandatory vars
        }
    }
}
