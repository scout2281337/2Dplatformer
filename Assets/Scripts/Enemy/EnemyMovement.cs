using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed;
    public Transform player;

    protected Vector3 previousPosition;
    public Animator anim;
    protected float speedX;

    protected Vector3 TargetPoint;
    protected bool isChasing;
    protected Rigidbody2D rb;

    protected bool facingRight = true; // Отслеживание направления, вправо ли смотрит персонаж
    public GameObject sprite;
    protected virtual void Start()
    {
        TargetPoint = pointB;
        previousPosition = transform.position;
    }

    protected virtual void Update()
    {
        if (isChasing)
        {
            Vector3 direction = new Vector3(player.position.x - transform.position.x, 0, 0).normalized;
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, TargetPoint, speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, TargetPoint) < 0.05f)
            {
                TargetPoint = TargetPoint == pointB ? pointA : pointB;
            }
        }

        speedX = (transform.position.x - previousPosition.x) / Time.deltaTime;
        previousPosition = transform.position;

        // Устанавливаем скорость для анимации с использованием Mathf.Abs()
        anim.SetFloat("xVelocity", Mathf.Abs(speedX));

        // Проверка направления движения и вызов Flip() при необходимости
        if ((speedX > 0 && !facingRight) || (speedX < 0 && facingRight))
        {
            Flip();
        }
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(pointA, pointB);
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isChasing = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isChasing = false;
        }
    }

    protected virtual void Flip()
    {
        // Инвертируем направление, меняя знак флага facingRight
        facingRight = !facingRight;

        // Инвертируем масштаб персонажа по оси X
        Vector3 theScale = sprite.transform.localScale;
        theScale.x *= -1;
        sprite.transform.localScale = theScale;
    }
}
