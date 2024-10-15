using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : Projectile
{
    
    public void SetGrenade(float speed, Vector2 diraction, float damage, float radius, float force)
    {
        SetExplosiveProjectile(speed, diraction, damage, radius, force);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Explosion();
    }
}
