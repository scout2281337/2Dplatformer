using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    public override bool WeaponAttack(Vector2 direction, GameObject player)
    {
        if (!base.WeaponAttack(direction, player)) return false;

        //shot
        GameObject projectile = Instantiate(weaponStats.projectileType, transform.position, Quaternion.identity); //Spawns bullet
        projectile.GetComponent<IProjectile>()?.SetProjectile(weaponStats, direction); //Sets bullets mandatory vars-
        
        return true;
    }
}
