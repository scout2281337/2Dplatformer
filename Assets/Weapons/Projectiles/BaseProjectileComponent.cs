using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProjectileComponent : ScriptableObject
{
    public abstract BaseProjectileComponent CloneComponent();

    public abstract float SetRandomStats(float min, float max);
}
