using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ImpactExplosiveComponent : MonoBehaviour, IProjectileImpactable
{
    public float explosionDamage;
    public float explosionRadius;
    public float explosionForce;
    public GameObject explosionPrefab;

    [SerializeField] private LayerMask explosionLayerMask; // Specify layers for the explosion

    public void ProjectileImpact(GameObject other)
    {
        // Instantiate explosion effect
        GameObject explosionFX = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionFX.transform.localScale *= 2 * explosionRadius;
        Debug.Log(gameObject.layer);
        // Use OverlapCircleAll with a layer mask
        Collider2D[] explosionCollisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayerMask);

        // Proceed only if there are any collisions
        if (explosionCollisions == null || explosionCollisions.Length == 0)
            return;

        foreach (var collision in explosionCollisions)
        {
            // Apply explosion force
            Push(collision);

            // Damage enemies
            DamageEnemy(collision);
        }
    }

    private void DamageEnemy(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            Debug.Log("Enemy hit by explosion");
            enemyHealth.TakeDamage((int)explosionDamage);
        }
    }

    private void Push(Collider2D collision)
    {
        IPushable pushable = collision.GetComponent<IPushable>();
        if (pushable != null)
        {
            Vector2 pushVector = (collision.transform.position - transform.position).normalized;
            pushable.Push(pushVector, explosionForce);
        }
    }
}
