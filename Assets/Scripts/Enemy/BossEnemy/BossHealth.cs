using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : EnemyHealth
{
    public BossAttack bossAttack;
    public BossMovement movement;
    private bool specialAttack = false;
    
    public override void TakeDamage(int damage) 
    {
        Currenthealth -= damage;
        //healthbar
        Healthbar.SetHealth(Currenthealth);

        if (Currenthealth <= 0)
        {
            Die();
        }
        if ( Currenthealth < 250 && !specialAttack) 
        {
            bossAttack.AllDirectionShoot();
            //movement.speed += 3;
            specialAttack = true;      
        }

    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.X)) 
        {
            TakeDamage(25);
        
        }
    }



}
