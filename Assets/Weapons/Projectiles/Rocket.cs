using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rocket : Projectile
{
    public LayerMask playerMask;
    public float projectileFullSpeed;
    private float projectileAcceleration = 0;
    private float explosionRadius;
    private float explosionForce;


    private void FixedUpdate()
    {
        rb.velocity = Vector2.Lerp(projectileDiraction.normalized * projectileSpeed, projectileDiraction.normalized * projectileFullSpeed, projectileAcceleration);
        projectileAcceleration += Time.deltaTime;
    }

    public void SetRocket(float speed, Vector2 diraction, float radius, float force, float fullSpeed)
    {
        SetProjectile(speed, diraction);
        explosionRadius = radius;
        explosionForce = force;
        projectileFullSpeed = fullSpeed;
    } 

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Explosion();
    }

    private void Explosion()
    {
        //Debug.Log("explosion");
        Collider2D explosionCollision = Physics2D.OverlapCircle(transform.position, explosionRadius, playerMask);
        if (explosionCollision != null)
        {
            if(explosionCollision.gameObject.tag == "Player")
            {
                Vector2 playerVector = explosionCollision.transform.position - transform.position;
                explosionCollision.gameObject.GetComponent<PlayerMovement>().PlayerAddForce(playerVector, explosionForce);
            }
        }
    }
}
