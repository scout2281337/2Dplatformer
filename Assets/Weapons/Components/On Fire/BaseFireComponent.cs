using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseFireComponent : BaseWeaponComponent
{
    public abstract void WeaponFire(PlayerMovement playerMovement, Vector2 projectileDiraction);
}
