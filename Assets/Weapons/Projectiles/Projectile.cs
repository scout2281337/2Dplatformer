using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour, IProjectile
{
    [Header("Projectile")]
    public Rigidbody2D rb;
    private WeaponStats_SO weaponStats;
    protected Vector2 projectileDiraction;


    public void SetProjectile(WeaponStats_SO newWeaponStats, Vector2 diraction)
    {
        weaponStats = newWeaponStats;
        projectileDiraction = diraction;

        float rotZ = Mathf.Atan2(projectileDiraction.y, projectileDiraction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        rb.velocity = projectileDiraction.normalized * weaponStats.projectileSpeed;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        foreach (BaseImpactComponent component in weaponStats.projectileComponents)
        {
            component.ProjectileImpact(other.gameObject, transform);
        }

        Destroy(gameObject);
    }
}
