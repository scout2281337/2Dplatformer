using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public int expGain = 100;
    private PlayerStats stats;
    private GameObject player;
    [SerializeField] protected float currentHealth;

    public HealthBar healthBar;

    protected virtual void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            stats = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("����� �� ������. ��������� ��� 'Player'.");
        }
    }

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);

        if (currentHealth <= 0)
        {
            stats.GainXP(expGain);
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }
}
