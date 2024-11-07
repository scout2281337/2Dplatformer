using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseImpactComponent : BaseProjectileComponent
{
    protected List<float> modifiers = new();

    public abstract void ProjectileImpact(GameObject other, Transform transform);

    protected float GetAvarageModifier()
    {
        return modifiers.Sum();
    }

    protected float GetModifier(float min, float max)
    {
        float modifier = Random.Range(min, max);
        modifiers.Add(modifier);
        return modifier;
    }
}
