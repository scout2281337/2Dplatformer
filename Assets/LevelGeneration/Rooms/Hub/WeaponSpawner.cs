using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public float minModifier;
    public float maxModifier;

    [SerializeField] private List<GameObject> impactComponents;
    [SerializeField] private GameObject[] WeaponType = new GameObject[4];

    private GameObject weaponObject;
    private Weapon weapon;
    private WeaponHandler weaponHandler;

    /// <summary>
    /// Instantiates weapon, proceduraly generates random stats
    /// </summary>
    public GameObject SpawnWeapon()
    {
        // For weapon rerol
        if (weaponObject != null)
        {
            Destroy(weaponObject);
        }

        // Instantiate random weapon
        GameObject randomWeapon = WeaponType[Random.Range(0, WeaponType.Length)];
        weaponObject = Instantiate(randomWeapon, transform.position, Quaternion.identity, transform);
        weapon = weaponObject.GetComponent<Weapon>();
        weaponHandler = weapon.weaponHandler.GetComponent<WeaponHandler>();
        weaponObject.GetComponent<Weapon>().weaponStats = GetNewRandomStats(weapon.weaponStats);

        return weaponObject;
    }
    /// <summary>
    /// Clones weaponStats and sets random stats
    /// </summary>
    private WeaponStats_SO GetNewRandomStats(WeaponStats_SO weaponStats)
    {
        WeaponStats_SO newStats = (WeaponStats_SO)weaponStats.Clone();

        List<float> costMods = new();
        for (int i = 0; i < newStats.projectileComponents.Count; i++)
        {
            costMods.Add(newStats.projectileComponents[i].SetRandomStats(minModifier, maxModifier));
        }
        SetBaseWeaponStats(costMods, newStats);

        weaponHandler.OnAddWeapon += WeaponDetach;

        return newStats;
    }
    /// <summary>
    /// Uses random range from min to max to randomly set the weapon, also sets cost with costMods
    /// </summary>
    public void SetBaseWeaponStats(List<float> costMods, WeaponStats_SO weaponStats)
    {
        float fireRateMod = Random.Range(minModifier, maxModifier);
        weaponStats.fireRate /= fireRateMod;

        float steamCostMod = Random.Range(minModifier, maxModifier);
        weaponStats.steamCost /= steamCostMod;

        costMods.Add((fireRateMod + steamCostMod) / 2);
        weaponStats.cost = (int)(weaponStats.cost * (costMods.Sum() / costMods.Count()));
    }
    //private void AddRandomComponent(WeaponStats_SO stats)
    //{
    //    GameObject randomImpactComponent = Instantiate(impactComponents[Random.Range(0, impactComponents.Count)]);
    //    randomImpactComponent.GetComponent<IProjectileComponent>()?.SetRandomStats(minModifier, maxModifier);

    //    stats.projectileComponents.Add(randomImpactComponent);
    //}

    private void WeaponDetach()
    {
        weaponHandler.OnAddWeapon -= WeaponDetach;
        weaponObject = null;
    }
}

