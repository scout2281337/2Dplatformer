using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    float xInput;
    public float speed;
    public float jumpForce;
    public int jumpAmount = 0;
    public int maxJumps = 2; // Максимум прыжков для двойного прыжка

    private Rigidbody2D rb;
    private bool canJump;

    public Animator anim;
    private bool facingRight = true; // Указывает, смотрит ли персонаж вправо

    

    public GameObject character;
    public GameObject groundCheck;
    public LayerMask groundMask;
    public float groundCheckRadius = 0.1f; // Радиус проверки касания земли

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        // Перемещение персонажа
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        // Обновление анимаций
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x)); // Используем Abs для анимации независимо от направления
        anim.SetFloat("yVelocity", rb.velocity.y);

        // Поворачиваем персонажа
        if (xInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (xInput < 0 && facingRight)
        {
            Flip();
        }

        // Прыжок
        if (Input.GetKeyDown("space") && canJump)
        {
            Jump();
        }

        CheckGround();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // Обновляем вертикальную скорость для прыжка
        jumpAmount++;
        anim.SetBool("isJumping", true); // Анимация прыжка
        //Debug.Log(canJump);
        if (jumpAmount >= maxJumps)
        {
            canJump = false; // Отключаем возможность прыгать, если исчерпаны прыжки
        }
    }

    private void CheckGround()
    {
        // Проверяем касание с землей с помощью OverlapCircle
        Collider2D groundCollision = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundMask);

        if (groundCollision != null) // Если персонаж на земле
        {
            canJump = true;
            jumpAmount = 0; // Сбрасываем количество прыжков
        }
        else
        {
            canJump = false;
        }

        anim.SetBool("isJumping", !canJump); // Когда на земле, прыжок выключен
        //Debug.Log(canJump);
    }

    private void Flip()
    {
        // Меняем направление движения персонажа
        facingRight = !facingRight;

        // Инвертируем локальную шкалу персонажа по оси X
        Vector3 theScale = character.transform.localScale;
        theScale.x *= -1; // Переворачиваем по оси X
        character.transform.localScale = theScale;
    }

    
    
}
