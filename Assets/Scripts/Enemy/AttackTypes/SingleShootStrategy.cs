using UnityEngine;

public class SingleShootStrategy : IAttackStrategy
{
    public void ExecuteAttack(Transform firePoint, GameObject projectilePrefab, Transform player, float projectileSpeed, int numberOfProjectiles)
    {
        GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = (player.position - firePoint.position).normalized;
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = direction * projectileSpeed;
    }
}