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

        SpawnProjectile(direction);
        
        return true;
    }
}
