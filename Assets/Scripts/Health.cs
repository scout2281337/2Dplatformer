using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int MaxHealth = 100;
    [SerializeField] private int Currenthealth;


    public Healthbar Healthbar;

    //public Animator anim;
    void Start()
    {
        Currenthealth = MaxHealth;
        Healthbar.SetMaxHealth(MaxHealth);
    }

    
    public void TakeDamage(int damage)
    {
        //anim.SetTrigger("Hurt");
        Currenthealth -= damage;
        //healthbar
        Healthbar.SetHealth(Currenthealth);

        if (Currenthealth <= 0)
        {
            Die();
        }

    }
    void Die()
    {
        Debug.Log("Герой умер");
    }

}
