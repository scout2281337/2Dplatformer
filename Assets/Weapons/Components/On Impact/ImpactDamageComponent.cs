using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "ImpactDamageComponent", menuName = "ScriptableObjects/Weapon/ImpactComponents/ImpactDamageComponent", order = 1)]
public class ImpactDamageComponent : BaseImpactComponent
{
    public float damage;

    public override void ProjectileImpact(GameObject other, Transform t)
    {
        if (EnemyManager.Instance.enemyIDamageable.TryGetValue(other, out IDamageable idamageable))
            idamageable.TakeDamage(damage);
    }

    public override BaseWeaponComponent CloneComponent()
    {
        ImpactDamageComponent newComponent = ScriptableObject.CreateInstance<ImpactDamageComponent>();
        newComponent.damage = damage;

        return newComponent;
    }

    public override float SetRandomStats(float min, float max)
    {
        damage *= GetModifier(min, max);

        return GetAvarageModifier();
    }
}
