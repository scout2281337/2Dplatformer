using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyScriptableObject enemyScriptableObject;
    
    [SerializeField] protected Transform firePoint;
    protected Transform player;
    protected bool canShoot = false;

    // Список стратегий атаки
    private List<IAttackStrategy> attackStrategies = new List<IAttackStrategy>();

    private void Start()
    {
        // Инициализируем стратегии
        attackStrategies.Add(new SingleShootStrategy());
        //attackStrategies.Add(new RandomShootStrategy());
        //attackStrategies.Add(new AllDirectionShootStrategy());

        // Запускаем стрельбу по таймеру
        InvokeRepeating(nameof(Shoot), 0f, enemyScriptableObject.fireRate);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            canShoot = true;
            Debug.Log("игрока видно");
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShoot = false;
        }
    }

    protected virtual void Shoot()
    {
        if (!canShoot) return;

        foreach (var strategy in attackStrategies)
        {
            // Выполняем каждую стратегию атаки
            strategy.ExecuteAttack(firePoint, enemyScriptableObject.projectilePrefab, player, enemyScriptableObject.projectileSpeed, enemyScriptableObject.numberOfProjectiles);
        }
    }
}
