using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProjectile
{
    void SetProjectile(float speed, Vector2 diraction, float damage);

    void AddExplosionComponent(float explosionDamage, float explosionRadius, float explosionForce);
}
