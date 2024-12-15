using UnityEngine;
using Zenject;

public class AttackLogic : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private GameObject explosionPrefab;
    [SerializeField] private GameObject hitBox;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            PlayerManager.Instance.playerHealth.TakeDamage(damage);

            Explode();
        }
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        CombatManager.Instance.RemoveEnemy(hitBox);
        Destroy(gameObject);
    }
}
