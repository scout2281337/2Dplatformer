using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public float recoilStrength;

    public override void WeaponAttack(Vector2 diraction, GameObject player)
    {
        base.WeaponAttack(diraction, player);

        if (canShoot)
        {
            //shot
            GameObject bullet = Instantiate(projectileType, transform.position, Quaternion.identity); //Spawns bullet
            bullet.GetComponent<Bullet>().SetBullet(projectileSpeed, diraction, damage); //Sets bullets mandatory vars

            //recoil
            player.GetComponent<IPushable>().Push(-1 * diraction, recoilStrength); // Pushes player in the firaction opposite of shooting
        }
    }
}
