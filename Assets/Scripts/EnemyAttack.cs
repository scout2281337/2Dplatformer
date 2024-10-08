using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Префаб шара
    public Transform firePoint;         // Точка, из которой будет вылетать снаряд
    public float fireRate = 1f;         // Скорость стрельбы (в секундах)
    public float projectileSpeed = 5f;  // Скорость полета шара
    private Transform player;           // Ссылка на игрока
    private bool canShoot = false;      // Переменная для проверки, может ли враг стрелять

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            canShoot = true;
            InvokeRepeating("Shoot", 0f, fireRate); // Запуск стрельбы через интервалы
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShoot = false;
            CancelInvoke("Shoot"); // Остановить стрельбу
        }
    }

    void Shoot()
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
}
