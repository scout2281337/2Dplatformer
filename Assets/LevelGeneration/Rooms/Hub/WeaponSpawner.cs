using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    private ComponentsList_SO componentsList;
    private float minModifier;
    private float maxModifier;
    private GameObject weaponObject;
    private Weapon weapon;
    private WeaponHandler weaponHandler;

    public void SetWeaponSpawner(float minMod, float maxMod, ComponentsList_SO newComponentsList)
    {
        minModifier = minMod;
        maxModifier = maxMod;
        componentsList = newComponentsList;
    }

    /// <summary>
    /// Instantiates weapon, proceduraly generates random stats
    /// </summary>
    public GameObject SpawnWeapon()
    {
        // For weapon reroll
        if (weaponObject != null)
        {
            Destroy(weaponObject);
        }

        // Instantiate random weapon
        GameObject randomWeapon = componentsList.weapons[Random.Range(0, componentsList.weapons.Count)];
        weaponObject = Instantiate(randomWeapon, transform.position, Quaternion.identity, transform);
        weapon = weaponObject.GetComponent<Weapon>();
        weaponHandler = weapon.weaponHandler.GetComponent<WeaponHandler>();

        weapon.weaponStats = GetNewRandomStats(weapon.weaponStats);

        return weaponObject;
    }

    /// <summary>
    /// Clones weaponStats and sets random stats
    /// </summary>
    private WeaponStats_SO GetNewRandomStats(WeaponStats_SO weaponStats)
    {
        WeaponStats_SO newStats = weaponStats.CloneStats();

        List<float> costMods = new();

        costMods.Add(newStats.shotTypeComponent.SetRandomStats(minModifier, maxModifier));
       
        foreach (BaseFireComponent component in newStats.fireComponents)
        {
            costMods.Add(component.SetRandomStats(minModifier, maxModifier));
        }

        foreach (BaseActiveComponent component in newStats.activeComponents)
        {
            costMods.Add(component.SetRandomStats(minModifier, maxModifier));
        }

        foreach (BaseImpactComponent component in newStats.impactComponents)
        {
            costMods.Add(component.SetRandomStats(minModifier, maxModifier));
        }

        SetBaseWeaponStats(costMods, newStats);

        weaponHandler.OnWeaponTaken += WeaponDetach;

        return newStats;
    }

    /// <summary>
    /// Uses random range from min to max to randomly set the weapon, also sets cost with costMods
    /// </summary>
    private void SetBaseWeaponStats(List<float> costMods, WeaponStats_SO weaponStats)
    {
        float fireRateMod = Random.Range(minModifier, maxModifier);
        weaponStats.fireRate /= fireRateMod;

        float steamCostMod = Random.Range(minModifier, maxModifier);
        weaponStats.steamCost /= steamCostMod;

        costMods.Add((fireRateMod + steamCostMod) / 2);
        weaponStats.cost = (int)(weaponStats.cost * (costMods.Sum() / costMods.Count()));
    }

    private void WeaponDetach()
    {
        weaponHandler.OnWeaponTaken -= WeaponDetach;
        weaponObject = null;
    }
}

