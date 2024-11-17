using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject explosionPrefab; 
    public float damage = 10;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == PlayerManager.Instance.player)
        {
            PlayerManager.Instance.playerHealth.TakeDamage(damage);
        }

        Explode();
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
