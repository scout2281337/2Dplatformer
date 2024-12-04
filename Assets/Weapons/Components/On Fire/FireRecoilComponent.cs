using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireRecoil", menuName = "ScriptableObjects/Weapon/FireComponent/FireRecoil", order = 1)]
public class FireRecoilComponent : BaseFireComponent
{
    [SerializeField] private float force;

    public override void WeaponFire(Vector2 projectileDirection)
    {
        PlayerManager.Instance.playerMovement.Push(-projectileDirection, force);
    }

    public override float SetRandomStats(float min, float max)
    {
        force *= Mathf.Sqrt(GetModifier(min, max));

        return GetAvarageModifier();
    }

    public override BaseWeaponComponent CloneComponent()
    {
        FireRecoilComponent newComponent = ScriptableObject.CreateInstance<FireRecoilComponent>();
        newComponent.force = force;

        return newComponent;
    }

    public override string GetDesription()
    {
        return ("\nОтталкивает игрока в противополжное направление" +
                $"\n Сила: {force}");
    }
}
