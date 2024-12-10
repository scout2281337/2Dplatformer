using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveShockerComponent", menuName = "ScriptableObjects/Weapon/ActiveComponents/ActiveShockerComponent", order = 1)]

public class ActiveAttractorComponentds : BaseActiveComponent
{
    public float damage;
    public float radius;

    public LayerMask layerMask;

    public override bool ActiveProjectile(Vector2 position)
    {
        if(!base.ActiveProjectile(position))
            return false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius, layerMask);
        foreach (Collider2D collider in colliders)
        {
            if(CombatManager.Instance.idamageableDict.TryGetValue(collider.gameObject, out IDamageable damageable))
                damageable.TakeDamage(damage);
        }

        return true;
    }

    public override BaseWeaponComponent CloneComponent()
    {
        ActiveAttractorComponentds newComponent = ScriptableObject.CreateInstance<ActiveAttractorComponentds>();
        newComponent.damage = damage;
        newComponent.radius = radius;
        newComponent.ActivationFrequency = ActivationFrequency;

        return newComponent;
    }

    public override string GetDesription()
    {
        return ("\nНаносит урон врагам с определённой периодичностью" +
                $"\n Урон: {damage}" +
                $"\n Радиус: {radius}");
    }

    public override float SetRandomStats(float min, float max)
    {
        damage += GetModifier(min, max);
        radius += GetModifier(min, max);

        return GetAvarageModifier();
    }
}
