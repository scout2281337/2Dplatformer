using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    private PlayerStats stat; // Компонент для ссылок на PlayerStats
    private GameObject player; // Переменная для ссылки на игрока
    public int ExpGain = 100;
    [SerializeField] protected int Currenthealth;

    public Healthbar Healthbar;

    protected virtual void Start()
    {
        // Устанавливаем здоровье и здоровье на healthbar
        Currenthealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);

        // Находим игрока по тегу
        player = GameObject.FindGameObjectWithTag("Player");

        // Если игрок найден, то получаем его компонент PlayerStats
        if (player != null)
        {
            stat = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("Игрок не найден. Проверьте тег 'Player'.");
        }
    }

    public virtual void TakeDamage(int damage)
    {
        // Уменьшаем здоровье
        Currenthealth -= damage;

        // Обновляем healthbar
        Healthbar.SetHealth(Currenthealth);

        // Проверяем, если здоровье <= 0, враг умирает
        if (Currenthealth <= 0)
        {
            // Если компонент PlayerStats найден, добавляем опыт
            if (stat != null)
            {
                stat.GainXP(ExpGain);
            }
            Die();
        }
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    public int GetHealth()
    {
        return Currenthealth;
    }
}
