using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    public float recoilStrength;

    public override void WeaponAttack(Vector2 diraction, GameObject player)
    {
        if (canFire && lastTimeShot + fireRate < Time.time)
        {
            //shot
            GameObject bullet = Instantiate(projectileType, transform.position, Quaternion.identity); //Spawns bullet
            bullet.GetComponent<Projectile>().SetProjectile(projectileSpeed, diraction); //Sets bullets mandatory vars
            lastTimeShot = Time.time; // gets time to compare then shooting to preserve firerate

            //recoil
            player.GetComponent<PlayerMovement>().PlayerAddForce(-1 * diraction, recoilStrength); // Pushes player in the firaction opposite of shooting
        }
    }
}
