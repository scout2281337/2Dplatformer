using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ImpactDamageComponent : MonoBehaviour, IProjectileImpactable
{
    public float damage;

    public void ProjectileImpact(GameObject other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(Mathf.RoundToInt(damage)); //TODO change int in player health
        }
    }
}
