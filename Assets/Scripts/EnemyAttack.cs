using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб снаряда
    public Transform firePoint;         // Точка, из которой будет вылетать снаряд
    public float fireRate = 1f;         // Скорость стрельбы
    public float projectileSpeed = 5f;  // Скорость полета снаряда
    public int NumberOfProjectiles; // количество снарядов
    protected Transform player;         // Ссылка на игрока
    protected bool canShoot = false;    // Может ли враг стрелять

    // Метод для включения режима атаки
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            canShoot = true;
            //StartShooting(); // Запуск стрельбы
            AllDirectionShoot();
        }
    }

    // Метод для отключения режима атаки
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShoot = false;
            //StopShooting(); // Остановить стрельбу
        }
    }

    // Начало стрельбы с интервалом
    protected virtual void StartShooting()
    {
        InvokeRepeating("Shoot", 0f, fireRate);
    }

    // Остановка стрельбы
    protected virtual void StopShooting()
    {
        CancelInvoke("Shoot");
    }

    // Основная логика стрельбы
    protected virtual void Shoot()
    {
        if (!canShoot) return;

        // Создаем снаряд
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Рассчитываем направление к игроку
        Vector2 direction = (player.position - firePoint.position).normalized;

        // Добавляем скорость снаряду
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }

    protected virtual void AllDirectionShoot()
    {
        if (!canShoot) return;

        float angleStep = 180f / (NumberOfProjectiles - 1);  // Угол между снарядами
        float angle = -90f;  // Начинаем от угла -90 (вверх), и будем двигаться вправо

        for (int i = 0; i < NumberOfProjectiles; i++)
        {
            // Рассчитываем направление на основе угла
            float projectileDirXPosition = Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirYPosition = Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 direction = new Vector2(projectileDirXPosition, projectileDirYPosition).normalized;

            // Создаем снаряд
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // Добавляем скорость снаряду
            rb.velocity = direction * projectileSpeed;

            // Увеличиваем угол для следующего снаряда
            angle += angleStep;
        }
    }

}
