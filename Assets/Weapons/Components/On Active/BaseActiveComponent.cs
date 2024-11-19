using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseActiveComponent : BaseWeaponComponent
{
    [SerializeField] protected int ActivationFrequency;
    protected int ActivationsNum;

    public virtual bool ActiveProjectile(Vector2 position)
    {
        ActivationsNum++;
        if (ActivationsNum == ActivationFrequency)
        {
            ActivationsNum = 0;
            return true;
        }

        return false;
    }
}
