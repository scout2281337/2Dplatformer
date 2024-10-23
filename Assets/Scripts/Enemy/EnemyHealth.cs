using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    private PlayerStats stat; // ��������� ��� ������ �� PlayerStats
    private GameObject player; // ���������� ��� ������ �� ������
    public int ExpGain = 100;
    [SerializeField] protected int Currenthealth;

    public Healthbar Healthbar;

    protected virtual void Start()
    {
        // ������������� �������� � �������� �� healthbar
        Currenthealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);

        // ������� ������ �� ����
        player = GameObject.FindGameObjectWithTag("Player");

        // ���� ����� ������, �� �������� ��� ��������� PlayerStats
        if (player != null)
        {
            stat = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("����� �� ������. ��������� ��� 'Player'.");
        }
    }

    public virtual void TakeDamage(int damage)
    {
        // ��������� ��������
        Currenthealth -= damage;

        // ��������� healthbar
        Healthbar.SetHealth(Currenthealth);

        // ���������, ���� �������� <= 0, ���� �������
        if (Currenthealth <= 0)
        {
            // ���� ��������� PlayerStats ������, ��������� ����
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
