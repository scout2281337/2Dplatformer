using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int MaxHealth = 100;
    public PlayerStats stat;
    public int ExpGain = 100;
    [SerializeField] protected int Currenthealth;


    public Healthbar Healthbar;

    //public Animator anim;
    protected virtual void Start()
    {
        Currenthealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
    }


    public virtual void TakeDamage(int damage)
    {
        //anim.SetTrigger("Hurt");
        Currenthealth -= damage;
        //healthbar
        Healthbar.SetHealth(Currenthealth);

        if (Currenthealth <= 0)
        {
            stat.GainXP(ExpGain);
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
