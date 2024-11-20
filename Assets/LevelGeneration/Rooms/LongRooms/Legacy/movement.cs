using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : SoundManager
{
    float xInput;
    public float speed;
    public float jumpForce;
    public int jumpAmount = 0;
    public int maxJumps = 2; // �������� ������� ��� �������� ������

    private Rigidbody2D rb;
    private bool canJump;

    public Animator anim;
    private bool facingRight = true; // ���������, ������� �� �������� ������

    

    public GameObject character;
    public GameObject groundCheck;
    public LayerMask groundMask;
    public float groundCheckRadius = 0.1f; // ������ �������� ������� �����

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        xInput = Input.GetAxis("Horizontal");

        // ����������� ���������
        rb.velocity = new Vector2(xInput * speed, rb.velocity.y);

        // ���������� ��������
        anim.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x)); // ���������� Abs ��� �������� ���������� �� �����������
        anim.SetFloat("yVelocity", rb.velocity.y);

        // ������������ ���������
        if (xInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (xInput < 0 && facingRight)
        {
            Flip();
        }

        // ������
        if (Input.GetKeyDown("space") && canJump)
        {
            Jump();
            PlaySound(sounds[0], volume: 0.05f);
        }

        CheckGround();
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce); // ��������� ������������ �������� ��� ������
        jumpAmount++;
        anim.SetBool("isJumping", true); // �������� ������
        //Debug.Log(canJump);
        if (jumpAmount >= maxJumps)
        {
            canJump = false; // ��������� ����������� �������, ���� ��������� ������
        }
    }

    private void CheckGround()
    {
        // ��������� ������� � ������ � ������� OverlapCircle
        Collider2D groundCollision = Physics2D.OverlapCircle(groundCheck.transform.position, groundCheckRadius, groundMask);

        if (groundCollision != null) // ���� �������� �� �����
        {
            canJump = true;
            jumpAmount = 0; // ���������� ���������� �������
        }
        else
        {
            canJump = false;
        }

        anim.SetBool("isJumping", !canJump); // ����� �� �����, ������ ��������
        //Debug.Log(canJump);
    }

    private void Flip()
    {
        // ������ ����������� �������� ���������
        facingRight = !facingRight;

        // ����������� ��������� ����� ��������� �� ��� X
        Vector3 theScale = character.transform.localScale;
        theScale.x *= -1; // �������������� �� ��� X
        character.transform.localScale = theScale;
    }

    
    
}
