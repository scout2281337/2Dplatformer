using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public Dictionary<GameObject, IDamageable> idamageableDict = new();

    public void AddObject(GameObject obj)
    {
        if(obj.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            idamageableDict.Add(obj, damageable);
        }
    }
}
