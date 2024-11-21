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
    [SerializeField] private LayerMask explosionLayerMask;

    public override void ProjectileImpact(GameObject collidedObject, Transform impactTransform)
    {

        // Instantiate explosion effect
        GameObject explosionFX = Instantiate(explosionPrefab, impactTransform.position, Quaternion.identity);
        explosionFX.transform.localScale *= 2 * explosionRadius;

        // Use OverlapCircleAll with a layer mask
        Collider2D[] explosionCollisions = Physics2D.OverlapCircleAll(impactTransform.position, explosionRadius, explosionLayerMask);

        // Proceed only if there are any collisions
        if (explosionCollisions.Length == 0)
            return;

        foreach (var collision in explosionCollisions)
        {
            // Apply explosion force
            Push(collision, impactTransform.position);

            // Damage enemies
            DamageEnemy(collision);
        }
    }

    private void DamageEnemy(Collider2D collision)
    {
        if (!CombatManager.Instance.idamageableDict.TryGetValue(collision.gameObject, out IDamageable idamageable))
            return;

        idamageable.TakeDamage(explosionDamage);
    }

    private void Push(Collider2D collision, Vector3 position)
    {
        if (collision.gameObject != PlayerManager.Instance.player)
            return;

        Vector2 pushVector = (collision.transform.position - position).normalized;
        PlayerManager.Instance.playerMovement.Push(pushVector, explosionForce);

        Debug.Log($"Player Position: {collision.transform.position}");
        Debug.Log($"Explosion Position: {position}");
        Debug.Log($"Push Vector: {pushVector}");
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
