using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleBarrelShotgun : Weapon
{
    [Header("DoubleBarrelShotgun")]
    public float recoilStrength;
    public int numberOfPellets;
    public float spreadAngle;


    public override void WeaponAttack(Vector2 direction, GameObject player)
    {
        base.WeaponAttack(direction, player);

        if (canShoot)
        {
            // Calculate the base angle from the shooting direction
            float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Calculate the starting angle for the first pellet (spreadAngle is centered around direction)
            float startAngle = baseAngle - (spreadAngle / 2);

            // Calculate the step angle between pellets
            float angleStep = spreadAngle / (numberOfPellets - 1);

            for (int i = 0; i < numberOfPellets; i++)
            {
                // Calculate the current angle for this pellet
                float currentAngle = startAngle + (angleStep * i);

                // Convert the angle back into a direction vector
                float projectileDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);  // Use Mathf.Deg2Rad to convert degrees to radians
                float projectileDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

                Vector2 pelletDirection = new Vector2(projectileDirX, projectileDirY).normalized;

                // Instantiate and set up the bullet
                GameObject bullet = Instantiate(projectileType, transform.position, Quaternion.identity);
                bullet.GetComponent<Bullet>().SetBullet(projectileSpeed, pelletDirection, damage);
            }

            // Apply recoil to the player
            player.GetComponent<IPushable>().Push(-direction, recoilStrength);  // Push player in the opposite direction of shooting
        }
    }
}
