using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseShotTypeComponent : BaseWeaponComponent
{
    public abstract void Shoot(Vector2 direction, Vector3 position, WeaponStats_SO weaponStats);
}
