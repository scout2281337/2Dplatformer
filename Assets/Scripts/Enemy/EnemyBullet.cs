using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject explosionPrefab; // Префаб анимации взрыва

    void Update()
    {
        transform.Rotate(0, 0, 1);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Попадание в игрока");
            Explode(); // Воспроизводим анимацию взрыва
        }
        else if (other.gameObject.layer == 6)
        {
            
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
