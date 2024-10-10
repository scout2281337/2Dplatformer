using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // ������ �������
    public Transform firePoint;         // �����, �� ������� ����� �������� ������
    public float fireRate = 1f;         // �������� ��������
    public float projectileSpeed = 5f;  // �������� ������ �������
    public int NumberOfProjectiles; // ���������� ��������
    protected Transform player;         // ������ �� ������
    protected bool canShoot = false;    // ����� �� ���� ��������

    // ����� ��� ��������� ������ �����
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player = collision.transform;
            canShoot = true;
            //StartShooting(); // ������ ��������
            AllDirectionShoot();
        }
    }

    // ����� ��� ���������� ������ �����
    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            canShoot = false;
            //StopShooting(); // ���������� ��������
        }
    }

    // ������ �������� � ����������
    protected virtual void StartShooting()
    {
        InvokeRepeating("Shoot", 0f, fireRate);
    }

    // ��������� ��������
    protected virtual void StopShooting()
    {
        CancelInvoke("Shoot");
    }

    // �������� ������ ��������
    protected virtual void Shoot()
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

    protected virtual void AllDirectionShoot()
    {
        if (!canShoot) return;

        float angleStep = 180f / (NumberOfProjectiles - 1);  // ���� ����� ���������
        float angle = -90f;  // �������� �� ���� -90 (�����), � ����� ��������� ������

        for (int i = 0; i < NumberOfProjectiles; i++)
        {
            // ������������ ����������� �� ������ ����
            float projectileDirXPosition = Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirYPosition = Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector2 direction = new Vector2(projectileDirXPosition, projectileDirYPosition).normalized;

            // ������� ������
            GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();

            // ��������� �������� �������
            rb.velocity = direction * projectileSpeed;

            // ����������� ���� ��� ���������� �������
            angle += angleStep;
        }
    }

}
