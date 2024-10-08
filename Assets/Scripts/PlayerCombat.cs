using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : SoundManager
{
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask enemyLayer;


    public float AttackRange;
    public int attackDamage = 20;

    public GameObject sprite;
    public GameObject shurikenPrefab;  // Префаб сюрикена
    public Transform firePoint;         // Точка, откуда будет выпущен сюрикен
    public float shurikenSpeed = 10f;   // Скорость полета сюрикена
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ThrowShuriken();
        }
    }

    private void Attack()
    {
        animator.SetTrigger("attack");
        PlaySound(sounds[0]);

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, enemyLayer);

        foreach (Collider2D enemy in hitEnemies)
        {
            // Проверяем, что коллайдер не является триггером
            if (!enemy.isTrigger)
            {
                Debug.Log(enemy.name);
                EnemyHealth enemyComponent = enemy.GetComponent<EnemyHealth>();

                if (enemyComponent != null)
                {
                    enemyComponent.TakeDamage(attackDamage);
                }
                else
                {
                    Debug.Log("нет врагов");
                }
            }
        }
    }
    void ThrowShuriken()
    {
        PlaySound(sounds[1]);
        // Создаем сюрикен
        GameObject shuriken = Instantiate(shurikenPrefab, firePoint.position, Quaternion.identity);

        // Рассчитываем направление, в котором будет лететь сюрикен
        Vector2 direction = sprite.transform.localScale.x > 0 ? Vector2.right : Vector2.left; // Определяем направление на основе ориентации персонажа

        // Устанавливаем скорость для сюрикена
        shuriken.GetComponent<Rigidbody2D>().velocity = direction * shurikenSpeed;
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
}
