using UnityEngine;
using Zenject;

public class AttackLogic : MonoBehaviour
{
    [SerializeField] private int Damage;
    [SerializeField] private GameObject explosionPrefab;
    private PlayerHealth playerHealth;
    [Inject] 
    public void Constructor(PlayerHealth playerHealth) 
    {
        this.playerHealth = playerHealth;
    
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ( collision.gameObject.CompareTag("Player")) 
        {
            
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
