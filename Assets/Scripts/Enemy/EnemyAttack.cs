using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab;
    public Transform firePoint;
    public float fireRate = 1f;
    public float projectileSpeed = 5f;
    public int numberOfProjectiles;
    private Transform player;
    private bool canShoot = false;

    // Список стратегий атаки
    private List<IAttackStrategy> attackStrategies = new List<IAttackStrategy>();

    private void Start()
    {
        // Инициализируем стратегии
        attackStrategies.Add(new SingleShootStrategy());
        //attackStrategies.Add(new RandomShootStrategy());
        //attackStrategies.Add(new AllDirectionShootStrategy());

        // Запускаем стрельбу по таймеру
        InvokeRepeating(nameof(Shoot), 0f, fireRate);
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
            strategy.ExecuteAttack(firePoint, projectilePrefab, player, projectileSpeed, numberOfProjectiles);
        }
    }
}
