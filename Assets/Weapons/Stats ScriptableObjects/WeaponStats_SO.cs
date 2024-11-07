using Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStats", order = 1)]
public class WeaponStats_SO : ScriptableObject, ICloneable
{
    public string WeaponName;
    public float fireRate;
    public float projectileSpeed;
    public float steamCost;
    public float heatGain;
    public int cost;
    public GameObject projectileType;
    public List<BaseProjectileComponent> projectileComponents;

    public object Clone()
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
        newWeaponStats.projectileComponents = new List<BaseProjectileComponent>();

        // Copy and clone each component
        for (int i = 0; i < projectileComponents.Count; i++)
        {
            newWeaponStats.projectileComponents.Add(projectileComponents[i].CloneComponent());
        }

        return newWeaponStats;
    }
}
