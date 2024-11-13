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
    void Update()
    {
        
        



    }
    protected override void Shoot()
    {
        if (!canShoot) return;

        if (enemyHealth.currentHealth >= 300)
        {
            // Выполняем каждую стратегию атаки
            attackStrategies[0].ExecuteAttack(firePoint, projectilePrefab, player, projectileSpeed, numberOfProjectiles);
        }
        else if(enemyHealth.currentHealth >= 150) 
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
