using UnityEngine;

public class AllDirectionShootStrategy : IAttackStrategy
{
    public void ExecuteAttack(Transform firePoint, GameObject projectilePrefab, Transform player, float projectileSpeed, int numberOfProjectiles)
    {
        float angleStep = 180f / (numberOfProjectiles - 1);
        float angle = -90f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            float projectileDirX = Mathf.Sin((angle * Mathf.PI) / 180f);
            float projectileDirY = Mathf.Cos((angle * Mathf.PI) / 180f);
            Vector2 direction = new Vector2(projectileDirX, projectileDirY).normalized;

            GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;

            angle += angleStep;
        }
    }
}