using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public float recoilStrength;

    public override bool WeaponAttack(Vector2 direction, GameObject player)
    {
        if (!base.WeaponAttack(direction, player)) return false;

        SpawnProjectile(direction);

        //recoil
        player.GetComponent<IPushable>().Push(-1 * direction, recoilStrength); // Pushes player in the firaction opposite of shooting
        
        return true;
    }
}
