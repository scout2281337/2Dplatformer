using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsObject", menuName = "ScriptableObjects/EnemyStatsObject")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Скрипт Атаки")]
    public GameObject projectilePrefab;
    public float fireRate;
    public float projectileSpeed;
    public int numberOfProjectiles;
    
    [Header("Скрипт Здоровья")]
    public float maxHealth;
    public float steamGain;
    public int xpGain;
}
