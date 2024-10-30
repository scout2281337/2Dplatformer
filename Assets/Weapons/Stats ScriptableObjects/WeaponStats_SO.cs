using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStats", menuName = "ScriptableObjects/WeaponStats", order = 1)]
public class WeaponStats_SO : ScriptableObject
{
    public string WeaponName;
    public float fireRate;
    public float projectileSpeed;
    public float steamCost;
    public float heatGain;
    public GameObject projectileType;
    public List<GameObject> projectileComponents;
}
