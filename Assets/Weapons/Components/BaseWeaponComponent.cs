using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseWeaponComponent : ScriptableObject
{
    protected List<float> modifiers = new();

    /// <summary>
    /// Creates a deep clone of a WeaponComponent
    /// </summary>
    public abstract BaseWeaponComponent CloneComponent();

    /// <summary>
    /// Randomizes stats in a range between min and max.
    /// </summary>
    /// <returns>Avarage modifier of stats</returns>
    public abstract float SetRandomStats(float min, float max);

    public abstract string GetDesription();
    /// <summary>
    /// Gets avare of all modifiers
    /// </summary>
    protected float GetAvarageModifier()
    {
        return modifiers.Sum();
    }

    /// <summary>
    /// Returns modifier in range between min and max, and adds it to modifiers list
    /// </summary>
    protected float GetModifier(float min, float max)
    {
        float modifier = Random.Range(min, max);
        modifiers.Add(modifier);
        return modifier;
    }
}
