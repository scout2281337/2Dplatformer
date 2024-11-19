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
    private Vector2 projectileDirection;

    public void SetProjectile(WeaponStats_SO newWeaponStats, Vector2 direction)
    {
        weaponStats = newWeaponStats;
        projectileDirection = direction;

        float rotZ = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        rb.velocity = projectileDirection.normalized * weaponStats.projectileSpeed;

        foreach (BaseFireComponent component in weaponStats.fireComponents)
        {
            component.WeaponFire(projectileDirection);
        }
    }

    public void FixedUpdate()
    {
        foreach (BaseActiveComponent component in weaponStats.activeComponents)
        {
            component.ActiveProjectile(transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (BaseImpactComponent component in weaponStats.impactComponents)
        {
            component.ProjectileImpact(other.gameObject, transform);
        }

        Destroy(gameObject);
    }
}
