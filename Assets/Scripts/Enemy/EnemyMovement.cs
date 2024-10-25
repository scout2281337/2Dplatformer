using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    public Transform player;
    public Animator anim;
    public GameObject sprite;

    private bool isChasing;
    private bool facingRight = true;
    private Rigidbody2D rb;

    public LayerMask groundLayer;
    public Transform groundCheck;
    public float checkDistance = 0.5f; // Расстояние для проверки платформы

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isChasing)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // Запуск анимации в зависимости от скорости
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * speed, rb.velocity.y);
    }

    private void Patrol()
    {
        // Проверка наличия земли впереди
        bool isGroundAhead = Physics2D.Raycast(groundCheck.position, Vector2.down, checkDistance, groundLayer);

        if (!isGroundAhead)
        {
            Flip();
        }

        // Движение в текущем направлении
        rb.velocity = new Vector2(facingRight ? speed : -speed, rb.velocity.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isChasing = false;
        }
    }

    private void Flip()
    {
        facingRight = !facingRight;

        // Инвертируем масштаб персонажа по оси X
        Vector3 theScale = sprite.transform.localScale;
        theScale.x *= -1;
        sprite.transform.localScale = theScale;
    }
    private void OnDrawGizmos()
    {
        Vector3 DownLine = new Vector3(groundCheck.position.x, groundCheck.position.y - checkDistance, groundCheck.position.z);
        Gizmos.DrawLine(groundCheck.position, DownLine);
    }
}
