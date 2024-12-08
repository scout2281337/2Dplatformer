using UnityEngine;

public class RandomShootStrategy : IAttackStrategy
{
    public void ExecuteAttack(Transform firePoint, GameObject projectilePrefab, Transform player, float projectileSpeed, int numberOfProjectiles)
    {
        for (int i = 0; i < numberOfProjectiles; i++)
        {
            Vector2 direction = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
            GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;
        }
    }
}