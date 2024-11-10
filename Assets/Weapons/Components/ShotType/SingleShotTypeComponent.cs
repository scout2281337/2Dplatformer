using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SingleShotTypeComponent", menuName = "ScriptableObjects/Weapon/ShotTypeComponents/SingleShotTypeComponent", order = 1)]
public class SingleShotTypeComponent : BaseShotTypeComponent
{
    public float inaccuracy;

    public override void Shoot(Vector2 direction, Vector3 position, WeaponStats_SO weaponStats, PlayerMovement playerMovement)
    {
        // Calculate a random angle offset within the range of -inaccuracy to +inaccuracy
        float angleOffset = Random.Range(-inaccuracy, inaccuracy);

        // Rotate the direction vector by the random angle offset
        Vector2 inaccurateDirection = Quaternion.Euler(0, 0, angleOffset) * direction;

        // Spawn and set up the projectile with the inaccurate direction
    }

    public override BaseWeaponComponent CloneComponent()
    {
        SingleShotTypeComponent newComponent = ScriptableObject.CreateInstance<SingleShotTypeComponent>();
        newComponent.inaccuracy = inaccuracy;

        return newComponent;
    }

    public override float SetRandomStats(float min, float max)
    {
        inaccuracy /= GetModifier(min, max);

        return GetAvarageModifier();
    }
}
