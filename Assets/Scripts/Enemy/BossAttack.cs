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
        InvokeRepeating(nameof(Shoot), 0f, enemyScriptableObject.fireRate);
    }

    // Update is called once per frame
    
    protected override void Shoot()
    {
        if (!canShoot) return;

        if (enemyHealth.currentHealth >= 0.8 * enemyScriptableObject.maxHealth)
        {
            enemyScriptableObject.projectileSpeed = 7f;
            attackStrategies[0].ExecuteAttack(firePoint, enemyScriptableObject.projectilePrefab, player, enemyScriptableObject.projectileSpeed, enemyScriptableObject.numberOfProjectiles);
        }
        else if(enemyHealth.currentHealth >= 0.5 * enemyScriptableObject.maxHealth) 
        {
            enemyScriptableObject.fireRate = 0.8f;
            attackStrategies[1].ExecuteAttack(firePoint, enemyScriptableObject.projectilePrefab, player, enemyScriptableObject.projectileSpeed, enemyScriptableObject.numberOfProjectiles);


        }
        else 
        {
            enemyScriptableObject.fireRate = 0.7f;
            enemyScriptableObject.numberOfProjectiles = 8;
            attackStrategies[2].ExecuteAttack(firePoint, enemyScriptableObject.projectilePrefab, player, enemyScriptableObject.projectileSpeed, enemyScriptableObject.numberOfProjectiles);



        }
        
    }
}
