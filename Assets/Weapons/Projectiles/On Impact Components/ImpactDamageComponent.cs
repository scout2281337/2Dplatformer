using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "ImpactDamageComponent", menuName = "ScriptableObjects/ImpactComponents/ImpactDamageComponent", order = 1)]
public class ImpactDamageComponent : BaseImpactComponent
{
    public float damage = 10f;

    public override void ProjectileImpact(GameObject other, Transform t)
    {
        other.GetComponent<EnemyHealth>()?.TakeDamage((int)damage);
    }

    public override BaseProjectileComponent CloneComponent()
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
