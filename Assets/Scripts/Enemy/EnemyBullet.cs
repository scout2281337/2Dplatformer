using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject explosionPrefab; 
    public int damage = 10; 

    void Update()
    {
        transform.Rotate(0, 0, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Попадание в игрока
            Health playerHealth = other.GetComponent<Health>();

            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); 
            }

            Explode(); 
        }
        else if (other.gameObject.layer == 6)
        {
            Explode(); 
        }
    }

    private void Explode()
    {
        
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        
        Destroy(gameObject);
    }
}
