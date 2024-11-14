using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : EnemyAttack
{
    private EnemyHealth enemyHealth;

    private List<IAttackStrategy> attackStrategies = new List<IAttackStrategy>();
    void Start()
    {
        enemyHealth = GetComponent<EnemyHealth>();
        attackStrategies.Add(new SingleShootStrategy());
        attackStrategies.Add(new RandomShootStrategy());
        attackStrategies.Add(new AllDirectionShootStrategy());
        InvokeRepeating(nameof(Shoot), 0f, fireRate);
    }

    // Update is called once per frame
    
    protected override void Shoot()
    {
        if (!canShoot) return;

        if (enemyHealth.currentHealth >= 0.8 * enemyHealth.maxHealth)
        {
            projectileSpeed = 7f;
            attackStrategies[0].ExecuteAttack(firePoint, projectilePrefab, player, projectileSpeed, numberOfProjectiles);
        }
        else if(enemyHealth.currentHealth >= 0.5 * enemyHealth.maxHealth) 
        {
            fireRate = 0.8f;
            attackStrategies[1].ExecuteAttack(firePoint, projectilePrefab, player, projectileSpeed, numberOfProjectiles);


        }
        else 
        {
            fireRate = 0.7f;
            numberOfProjectiles = 8;
            attackStrategies[2].ExecuteAttack(firePoint, projectilePrefab, player, projectileSpeed, numberOfProjectiles);



        }
        
    }
}
