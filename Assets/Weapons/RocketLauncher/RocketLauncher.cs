using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Weapon
{
    [Header("RocketLauncher")]
    public float explosionRadius;
    public float explosionForce;

    public override void WeaponAttack(Vector2 diraction, GameObject player)
    {
        if (canFire && lastTimeShot + fireRate < Time.time)
        {
            //shot
            GameObject bullet = Instantiate(projectileType, transform.position, Quaternion.identity); //Spawns bullet
            bullet.GetComponent<Rocket>().SetRocket(projectileSpeed, diraction, explosionRadius, explosionForce); //Sets bullets mandatory vars
            lastTimeShot = Time.time; // gets time to compare when shooting to preserve firerate
        }
    }
}
