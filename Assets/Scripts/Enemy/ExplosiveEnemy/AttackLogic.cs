using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLogic : MonoBehaviour
{
    public GameObject explosionPrefab;
    public float Damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.tag == "Player") 
        {
            Debug.Log("Попадание в игрока");
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
