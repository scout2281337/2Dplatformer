using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [Header("Scriptable Object")]
    public EnemyScriptableObject enemyScriptableObject;
    
    public float currentHealth;
    [SerializeField] private GameObject steamPickUp;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        CombatManager.Instance.AddEnemy(transform.parent.gameObject, this);

        currentHealth = enemyScriptableObject.maxHealth;
        healthBar.SetMaxHealth(enemyScriptableObject.maxHealth);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
            Die();
    }

    private void Die()
    {
        PlayerManager.Instance.AddXpToPlayer(enemyScriptableObject.xpGain);
        CombatManager.Instance.RemoveEnemy(transform.parent.gameObject);

        SpawnSteamPickUp();

        Destroy(transform.parent.gameObject);
    }

    private void SpawnSteamPickUp()
    {
        GameObject newSteamPickUp = Instantiate(steamPickUp, transform.position, Quaternion.identity);
        if (newSteamPickUp.TryGetComponent<SteamPickUp>(out SteamPickUp newSteamScript))
            newSteamScript.steamAmount = enemyScriptableObject.steamGain;
    }
}
