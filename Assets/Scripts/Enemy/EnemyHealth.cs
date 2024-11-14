using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public float maxHealth = 100f;
    public float currentHealth;
    [SerializeField] private int xpGain = 100;
    [SerializeField] private float steamGain;

    [SerializeField] private GameObject steamPickUp;
    [SerializeField] private HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
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
        PlayerManager.instance.AddXpToPlayer(xpGain);
        SpawnSteamPickUp();

        Destroy(gameObject);
    }

    private void SpawnSteamPickUp()
    {
        GameObject newSteamPickUp = Instantiate(steamPickUp, transform.position, Quaternion.identity);
        if (newSteamPickUp.TryGetComponent<SteamPickUp>(out SteamPickUp newSteamScript))
            newSteamScript.steamAmount = steamGain;
    }
}
