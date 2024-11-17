using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : Singleton<EnemyManager>
{
    public Dictionary<GameObject, IDamageable> enemyIDamageable = new();

    public void AddEnemy(GameObject enemy)
    {
        if(enemy.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            enemyIDamageable.Add(enemy, damageable);
        }
    }
}
