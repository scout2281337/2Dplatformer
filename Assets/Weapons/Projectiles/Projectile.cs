using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{
    protected float projectileSpeed;
    protected Vector2 projectileDiraction;
    public Rigidbody2D rb;


    public virtual void SetProjectile(float speed, Vector2 diraction)
    {
        projectileSpeed = speed;
        projectileDiraction = diraction;
        float rotZ = Mathf.Atan2(projectileDiraction.y, projectileDiraction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(gameObject);
    }
}
