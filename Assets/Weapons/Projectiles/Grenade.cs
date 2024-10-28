using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    
    public void SetGrenade(float speed, Vector2 diraction, float damage, float explDamage, float explRadius, float explForce)
    {
        SetExplosiveProjectile(speed, diraction, damage, explDamage, explRadius, explForce);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Explosion();
    }
}
