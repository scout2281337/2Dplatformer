using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    [Header("RocketLauncher")]
    public float explosionRadius;
    public float explosionForce;

    public override bool WeaponAttack(Vector2 direction, GameObject player)
    {
        if (!base.WeaponAttack(direction, player)) return false;

        //shot
        GameObject bullet = Instantiate(projectileType, transform.position, Quaternion.identity); //Spawns bullet
        bullet.GetComponent<Rocket>().SetRocket(projectileSpeed, direction, damage, explosionRadius, explosionForce); //Sets bullets mandatory vars
        return true;
    }
}
