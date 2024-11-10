using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ImpactExplosiveComponent", menuName = "ScriptableObjects/Weapon/ImpactComponents/ImpactExplosiveComponent", order = 1)]
public class ImpactExplosiveComponent : BaseImpactComponent
{
    public float explosionDamage;
    public float explosionRadius;
    public float explosionForce;

    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private LayerMask explosionLayerMask; // Specify layers for the explosion

    public override void ProjectileImpact(GameObject other, Transform transform)
    {

        // Instantiate explosion effect
        GameObject explosionFX = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        explosionFX.transform.localScale *= 2 * explosionRadius;

        // Use OverlapCircleAll with a layer mask
        Collider2D[] explosionCollisions = Physics2D.OverlapCircleAll(transform.position, explosionRadius, explosionLayerMask);

        // Proceed only if there are any collisions
        if (explosionCollisions == null || explosionCollisions.Length == 0)
            return;

        foreach (var collision in explosionCollisions)
        {
            // Apply explosion force
            Push(collision, transform);

            // Damage enemies
            DamageEnemy(collision);
        }
    }

    private void DamageEnemy(Collider2D collision)
    {
        if (!collision.TryGetComponent<EnemyHealth>(out var enemyHealth))
            return;

        enemyHealth.TakeDamage((int)explosionDamage);
    }

    private void Push(Collider2D collision, Transform transform)
    {
        if (!collision.TryGetComponent<IPushable>(out var pushable))
            return;
        
        Vector2 pushVector = (collision.transform.position - transform.position).normalized;
        pushable.Push(pushVector, explosionForce);
        
    }

    public override float SetRandomStats(float min, float max)
    {
        explosionDamage *= GetModifier(min, max);
        explosionRadius *= GetModifier(min, max);

        return GetAvarageModifier();
    }

    public override BaseWeaponComponent CloneComponent()
    {
        ImpactExplosiveComponent newComponent = ScriptableObject.CreateInstance<ImpactExplosiveComponent>();
        newComponent.explosionDamage = explosionDamage;
        newComponent.explosionRadius = explosionRadius;
        newComponent.explosionForce = explosionForce;
        newComponent.explosionPrefab = explosionPrefab;
        newComponent.explosionLayerMask = explosionLayerMask;

        return newComponent;
    }
}
