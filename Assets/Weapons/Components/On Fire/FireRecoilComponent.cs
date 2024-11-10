using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FireRecoil", menuName = "ScriptableObjects/Weapon/FireComponent/FireRecoil", order = 1)]
public class FireRecoilComponent : BaseFireComponent
{
    [SerializeField] private float recoilForce;
    public override void WeaponFire(PlayerMovement playerMovement, Vector2 projectileDiraction)
    {
        playerMovement.Push(-projectileDiraction, recoilForce);
    }

    public override float SetRandomStats(float min, float max)
    {
        recoilForce *= Mathf.Sqrt(GetModifier(min, max));

        return GetAvarageModifier();
    }

    public override BaseWeaponComponent CloneComponent()
    {
        FireRecoilComponent newComponent = ScriptableObject.CreateInstance<FireRecoilComponent>();
        newComponent.recoilForce = recoilForce;

        return newComponent;
    }
}
