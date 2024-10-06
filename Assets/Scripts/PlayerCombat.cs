using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : SoundManager
{
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask enemyLayer;


    public float AttackRange;
    public int attackDamage = 20;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        
        } 
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        PlaySound(sounds[0]);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayer);

        foreach ( Collider2D enemy in hitEnemies) 
        {
            EnemyHealth enemyComponent = enemy.GetComponent<EnemyHealth>();

            if (enemyComponent != null) 
            {
                enemyComponent.TakeDamage(attackDamage);
            }
            else 
            {
                Debug.Log("��������� �� ������");
            }


        
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
