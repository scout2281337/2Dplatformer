using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/Weapon/WeaponStats", order = 1)]
public class WeaponStats_SO : ScriptableObject
{
    public string WeaponName;
    public float fireRate;
    public float projectileSpeed;
    public float steamCost;
    public float heatGain;
    public int cost;
    public GameObject projectileType;
    public BaseShotTypeComponent shotTypeComponent;
    public List<BaseFireComponent> fireComponents;
    public List<BaseActiveComponent> activeComponents;
    public List<BaseImpactComponent> impactComponents;

    public WeaponStats_SO CloneStats()
    {
        WeaponStats_SO newWeaponStats = ScriptableObject.CreateInstance<WeaponStats_SO>();

        // Copy primitive and reference type fields
        newWeaponStats.WeaponName = WeaponName;
        newWeaponStats.fireRate = fireRate;
        newWeaponStats.projectileSpeed = projectileSpeed;
        newWeaponStats.steamCost = steamCost;
        newWeaponStats.heatGain = heatGain;
        newWeaponStats.cost = cost;
        newWeaponStats.projectileType = projectileType;
        newWeaponStats.shotTypeComponent = shotTypeComponent;
        newWeaponStats.fireComponents = new();
        newWeaponStats.activeComponents = new();
        newWeaponStats.impactComponents = new();


        // Copy and clone each component
        
        newWeaponStats.shotTypeComponent = (BaseShotTypeComponent)shotTypeComponent.CloneComponent();

        for (int i = 0; i < fireComponents.Count; i++)
        {
            newWeaponStats.fireComponents.Add((BaseFireComponent)fireComponents[i].CloneComponent());
        }

        for (int i = 0; i < activeComponents.Count; i++)
        {
            newWeaponStats.activeComponents.Add((BaseActiveComponent)activeComponents[i].CloneComponent());
        }

        for (int i = 0; i < impactComponents.Count; i++)
        {
            newWeaponStats.impactComponents.Add((BaseImpactComponent)impactComponents[i].CloneComponent());
        }

        return newWeaponStats;
    }
}
