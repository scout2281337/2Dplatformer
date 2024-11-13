using UnityEngine;

public class AllDirectionShootStrategy : IAttackStrategy
{
    public void ExecuteAttack(Transform firePoint, GameObject projectilePrefab, Transform player, float projectileSpeed, int numberOfProjectiles)
    {
        // ”гол распредел€етс€ по 360 градусов
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // –ассчитываем направление выстрела дл€ текущего угла
            float projectileDirX = Mathf.Sin(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Cos(angle * Mathf.Deg2Rad);
            Vector2 direction = new Vector2(projectileDirX, projectileDirY).normalized;

            // —оздаем снар€д и направл€ем его
            GameObject projectile = GameObject.Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            rb.velocity = direction * projectileSpeed;

            // ”величиваем угол дл€ следующего снар€да
            angle += angleStep;
        }
    }
}
