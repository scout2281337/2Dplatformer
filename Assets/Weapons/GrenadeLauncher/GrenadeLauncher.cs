using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    [Header("GrenadeLauncher")]
    public float explosionDamage;
    public float explosionRadius;
    public float explosionForce;


    public override bool WeaponAttack(Vector2 direction, GameObject player)
    {
        if (!base.WeaponAttack(direction, player)) 
            return false;

        //shot
        GameObject projectile = Instantiate(weaponStats.projectileType, transform.position, Quaternion.identity); //Spawns bullet
        IProjectile iprojectile = projectile.GetComponent<IProjectile>();

        iprojectile.SetProjectile(weaponStats, direction); //Sets bullets mandatory 
        
        return true;
    }
}
