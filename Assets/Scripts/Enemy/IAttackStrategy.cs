using UnityEngine;

public interface IAttackStrategy
{
    void ExecuteAttack(Transform firePoint, GameObject projectilePrefab, Transform player, float projectileSpeed, int numberOfProjectiles);
}
