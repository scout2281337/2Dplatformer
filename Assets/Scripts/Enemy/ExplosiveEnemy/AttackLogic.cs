using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogic : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player") 
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(Damage);
            }
            Explode(); // Воспроизводим анимацию взрыва
        }
    }

    private void Explode()
    {

        // Создайте эффект взрыва в позиции текущего объекта
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        // Уничтожьте пулю
        Destroy(gameObject);

    }
}
