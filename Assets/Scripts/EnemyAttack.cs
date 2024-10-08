using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // ������ ����
    public Transform firePoint;         // �����, �� ������� ����� �������� ������
    public float fireRate = 1f;         // �������� �������� (� ��������)
    public float projectileSpeed = 5f;  // �������� ������ ����
    private Transform player;           // ������ �� ������
    private bool canShoot = false;      // ���������� ��� ��������, ����� �� ���� ��������

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            canShoot = true;
            InvokeRepeating("Shoot", 0f, fireRate); // ������ �������� ����� ���������
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShoot = false;
            CancelInvoke("Shoot"); // ���������� ��������
        }
    }

    void Shoot()
    {
        if (!canShoot) return;

        // ������� ������
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // ������������ ����������� � ������
        Vector2 direction = (player.position - firePoint.position).normalized;

        // ��������� �������� �������
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }
}
