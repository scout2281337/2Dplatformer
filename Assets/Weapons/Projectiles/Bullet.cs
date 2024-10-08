using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Bullet : Projectile
{

    private void FixedUpdate()
    {
        rb.velocity = projectileDiraction.normalized * projectileSpeed;
    }

    public void SetBullet(float speed, Vector2 diraction)
    {
        SetProjectile(speed, diraction);
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player") 
        {
            Debug.Log("Player hit");
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
