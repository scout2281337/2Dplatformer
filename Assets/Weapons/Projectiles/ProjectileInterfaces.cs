using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface IProjectile
{
    void SetProjectile(WeaponStats_SO weaponStats, Vector2 diraction);
}

public interface IProjectileImpactable
{
    void ProjectileImpact(GameObject other);
}
