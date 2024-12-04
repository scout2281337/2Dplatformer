using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpreadShotTypeComponent", menuName = "ScriptableObjects/Weapon/ShotTypeComponents/SpreadShotTypeComponent", order = 1)]
public class SpreadShotTypeComponent : BaseShotTypeComponent
{ 
    public int numberOfPellets;
    public float spreadAngle;

    public override BaseWeaponComponent CloneComponent()
    {
        SpreadShotTypeComponent newComponent = ScriptableObject.CreateInstance<SpreadShotTypeComponent>();
        newComponent.numberOfPellets = numberOfPellets;
        newComponent.spreadAngle = spreadAngle;

        return newComponent;
    }

    public override string GetDesription()
    {
        return ("\nВыстреивает несколько потронов в конусе" +
                $"\n Количество пуль: {numberOfPellets}" +
                $"\n Разброс: {spreadAngle}");
    }

    public override float SetRandomStats(float min, float max)
    {
        numberOfPellets += (int)GetModifier(min, max);
        spreadAngle /= GetModifier(min, max);

        return GetAvarageModifier();
    }

    public override void Shoot(Vector2 direction, Vector3 position, WeaponStats_SO weaponStats)
    {

        // Calculate the base angle from the shooting direction
        float baseAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Calculate the starting angle for the first pellet (spreadAngle is centered around direction)
        float startAngle = baseAngle - (spreadAngle / 2);

        // Calculate the step angle between pellets
        float angleStep = spreadAngle / (numberOfPellets - 1);

        for (int i = 0; i < numberOfPellets; i++)
        {
            // Calculate the current angle for this pellet
            float currentAngle = startAngle + (angleStep * i);

            // Convert the angle back into a direction vector
            float projectileDirX = Mathf.Cos(currentAngle * Mathf.Deg2Rad);  // Use Mathf.Deg2Rad to convert degrees to radians
            float projectileDirY = Mathf.Sin(currentAngle * Mathf.Deg2Rad);

            Vector2 pelletDirection = new Vector2(projectileDirX, projectileDirY).normalized;

            GameObject projectile = Instantiate(weaponStats.projectileType, position, Quaternion.identity);
            projectile.GetComponent<Projectile>()?.SetProjectile(weaponStats, pelletDirection);
        }
    }
}
 
