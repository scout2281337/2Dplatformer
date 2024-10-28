using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public float recoilStrength;

    public override bool WeaponAttack(Vector2 direction, GameObject player)
    {
        if (!base.WeaponAttack(direction, player)) return false;

        //shot
        GameObject projectile = Instantiate(projectileType, transform.position, Quaternion.identity); //Spawns bullet
        IProjectile iprojectile = projectile.GetComponent<IProjectile>();

        iprojectile.SetProjectile(projectileSpeed, direction, damage); //Sets bullets mandatory vars

        //recoil
        player.GetComponent<IPushable>().Push(-1 * direction, recoilStrength); // Pushes player in the firaction opposite of shooting
        
        return true;
    }
}
