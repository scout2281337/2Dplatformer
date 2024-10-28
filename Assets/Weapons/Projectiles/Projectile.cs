using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour, IProjectile
{
    [Header("Projectile")]
    public float projectileDamage;
    protected float projectileSpeed;
    protected Vector2 projectileDiraction;
    public Rigidbody2D rb;

    [Header("ExplosiveProjectile")]
    public LayerMask playerMask;
    public GameObject explosionPrefab;
    protected float explosionDamage;
    protected float explosionRadius;
    protected float explosionForce;


    public virtual void SetProjectile(float speed, Vector2 diraction, float damage)
    {
        projectileSpeed = speed;
        projectileDiraction = diraction;
        projectileDamage = damage;

        float rotZ = Mathf.Atan2(projectileDiraction.y, projectileDiraction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
        rb.velocity = projectileDiraction.normalized * projectileSpeed;
    }

    public void AddExplosionComponent(float damage, float radius, float force)
    {
        explosionDamage = damage;
        explosionRadius = radius;
        explosionForce = force;
    }
    public virtual void SetExplosiveProjectile(float speed, Vector2 diraction, float damage, float explDamage, float explRadius, float explForce)
    {
        SetProjectile(speed, diraction, damage);
        
    }

    protected virtual void Explosion()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        //Debug.Log("explosion");
        Collider2D explosionCollision = Physics2D.OverlapCircle(transform.position, explosionRadius, playerMask);
        if (explosionCollision != null)
        {
            EnemyHealth enemyHealth = explosionCollision.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //enemyHealth.TakeDamage();
            }
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
            enemyHealth.TakeDamage(Mathf.RoundToInt(projectileDamage)); //TODO change int in player health
        }
        Destroy(gameObject);
    }
}
