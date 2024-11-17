using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public Dictionary<GameObject, IDamageable> idamageable = new();

    public void AddObject(GameObject obj)
    {
        if(obj.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            idamageable.Add(obj, damageable);
        }
    }
}
