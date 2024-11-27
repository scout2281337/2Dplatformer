using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStatsObject", menuName = "ScriptableObjects/EnemyStatsObject")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Скрипт Атаки")]
    public float fireRate;
    public float projectileSpeed;
    public int numberOfProjectiles;
    public GameObject projectilePrefab;

    [Header("Скрипт Здоровья")]
    public float maxHealth;
    public float steamGain;
    public int xpGain;

    [Header("Скрипт передвижения")]
    public float speed;
    public LayerMask groundLayer;
    public float checkDistance;
}
