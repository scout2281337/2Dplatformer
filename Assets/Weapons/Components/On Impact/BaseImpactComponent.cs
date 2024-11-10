using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseImpactComponent : BaseWeaponComponent
{
    public abstract void ProjectileImpact(GameObject other, Transform transform);
}
