using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    public Rigidbody2D rb;
    private WeaponStats_SO weaponStats;
    private Vector2 projectileDiraction;
    private PlayerMovement playerMovement;

    public void SetProjectile(WeaponStats_SO newWeaponStats, Vector2 diraction, PlayerMovement newPlayer)
    {
        weaponStats = newWeaponStats;
        projectileDiraction = diraction;
        playerMovement = newPlayer;

        float rotZ = Mathf.Atan2(projectileDiraction.y, projectileDiraction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        rb.velocity = projectileDiraction.normalized * weaponStats.projectileSpeed;

        foreach (BaseFireComponent component in weaponStats.fireComponents.Cast<BaseFireComponent>())
        {
            component.WeaponFire(playerMovement, projectileDiraction);
        }
    }

    public void FixedUpdate()
    {
        foreach (BaseActiveComponent component in weaponStats.activeComponents.Cast<BaseActiveComponent>())
        {
            component.ActiveProjectile();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (BaseImpactComponent component in weaponStats.impactComponents.Cast<BaseImpactComponent>())
        {
            component.ProjectileImpact(other.gameObject, transform);
        }

        Destroy(gameObject);
    }
}
