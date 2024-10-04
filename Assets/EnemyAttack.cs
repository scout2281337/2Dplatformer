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
        if (collision.CompareTag("Player")) // Проверка, если объект — это игрок
        {
            player = collision.transform;
            canShoot = true;
            StartCoroutine(Shoot());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShoot = false;
            StopCoroutine(Shoot());
        }
    }

    IEnumerator Shoot()
    {
        while (canShoot)
        {
            // Создаем снаряд
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

            // Рассчитываем направление к игроку
            Vector2 direction = (player.position - firePoint.position).normalized;

            // Добавляем скорость снаряду
            projectile.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;

            // Ждем перед следующим выстрелом
            yield return new WaitForSeconds(fireRate);
        }
    }

    
}
