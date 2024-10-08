using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class PlayerCombat : SoundManager
{
    public Animator animator;
    public Transform AttackPoint;
    public LayerMask enemyLayer;

    public float AttackRange;
    public int attackDamage = 20;

    [Header("Aiming")]
    public Camera cam;
    public GameObject hand;
    private Vector3 mousePos;
    private Vector3 diractionVector;

    //public GameObject sprite;
    //public GameObject shurikenPrefab;  // Префаб сюрикена
    //public Transform firePoint;         // Точка, откуда будет выпущен сюрикен
    //public float shurikenSpeed = 10f;   // Скорость полета сюрикена


    void Update()
    {
        AimAtMouse();

        if (Input.GetMouseButtonDown(0))
        {
            Attack();
        
        }
        if (Input.GetKey(KeyCode.Q))
        {
            UseWeapon();
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

    private void AimAtMouse()
    {
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        diractionVector = mousePos - hand.transform.position;
        float rotZ = Mathf.Atan2(diractionVector.y, diractionVector.x) * Mathf.Rad2Deg;
        hand.transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    private void UseWeapon()
    {
        hand.transform.GetChild(0).GetComponent<Weapon>().WeaponAttack(diractionVector, gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;
        
        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }

    //void ThrowShuriken()
    //{
    //    PlaySound(sounds[1]);
    //    // Создаем сюрикен
    //    GameObject shuriken = Instantiate(shurikenPrefab, firePoint.position, Quaternion.identity);

    //    // Рассчитываем направление, в котором будет лететь сюрикен
    //    Vector2 direction = sprite.transform.localScale.x > 0 ? Vector2.right : Vector2.left; // Определяем направление на основе ориентации персонажа

    //    // Устанавливаем скорость для сюрикена
    //    shuriken.GetComponent<Rigidbody2D>().velocity = direction * shurikenSpeed;
    //}
}
