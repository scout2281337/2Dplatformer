using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    [Header("Projectile")]
    public int projectileDamage;
    public Rigidbody2D rb;
    protected float projectileSpeed;
    protected Vector2 projectileDiraction;

    [Header("ExplosiveProjectile")]
    public LayerMask playerMask;
    public GameObject explosionPrefab;
    protected float explosionRadius;
    protected float explosionForce;


    public virtual void SetProjectile(float speed, Vector2 diraction, int damage)
    {
        projectileSpeed = speed;
        projectileDiraction = diraction;
        projectileDamage = damage;
        float rotZ = Mathf.Atan2(projectileDiraction.y, projectileDiraction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        rb.velocity = projectileDiraction.normalized * projectileSpeed;
    }

    public virtual void SetExplosiveProjectile(float speed, Vector2 diraction, int damage, float radius, float force)
    {
        SetProjectile(speed, diraction, damage);
        explosionRadius = radius;
        explosionForce = force;
    }

    protected virtual void Explosion()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //Debug.Log("explosion");
        Collider2D explosionCollision = Physics2D.OverlapCircle(transform.position, explosionRadius, playerMask);
        if (explosionCollision != null)
        {
            IPushable pushable = explosionCollision.GetComponent<IPushable>();
            if (pushable != null)
            {
                Vector2 pushVector = (explosionCollision.transform.position - transform.position).normalized;
                pushable.Push(pushVector, explosionForce);
            }
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        EnemyHealth enemyHealth = other.gameObject.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(projectileDamage);
        }
        Destroy(gameObject);
    }
}
