using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ActiveAttractorComponent", menuName = "ScriptableObjects/Weapon/ActiveComponents/ActiveAttractorComponent", order = 1)]
public class ActiveAttractorComponent : BaseActiveComponent
{
    public float force;
    public float radius;

    public override bool ActiveProjectile(Vector2 position)
    {
        if (!base.ActiveProjectile(position))
            return false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D collider in colliders)
        {
            if(collider.gameObject != PlayerManager.Instance.player)
                continue;
            
            Vector2 forceVector = position - (Vector2)PlayerManager.Instance.player.transform.position;
            PlayerManager.Instance.playerMovement.Push(forceVector.normalized, force);
        }

        return true;
    }

    public override BaseWeaponComponent CloneComponent()
    {
        ActiveAttractorComponent newComponent = ScriptableObject.CreateInstance<ActiveAttractorComponent>();
        newComponent.force = force;
        newComponent.radius = radius;
        newComponent.ActivationFrequency = ActivationFrequency;

        return newComponent;
    }

    public override string GetDesription()
    {
        return ("\nПритягивает к себе игрока" +
                $"\n Сила: {force}" +
                $"\n Радиус: {radius}");
    }

    public override float SetRandomStats(float min, float max)
    {
        force += GetModifier(min, max);
        radius += GetModifier(min, max);

        return GetAvarageModifier();
    }
}
